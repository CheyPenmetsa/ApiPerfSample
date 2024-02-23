import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate } from 'k6/metrics';

export let errorRate = new Rate('errors');

export let options = {
    stages: [
        { duration: '2s', target: 1 },
        { duration: '5s', target: 2 },
        { duration: '2s', target: 0 },
    ],
};

export default function () {

    const id = createResident();
    sleep(1);

    if (id) {
        getResident(id);
    }

    sleep(2);
}

function getResident(id) {
    let response = http.get(`http://localhost:5132/api/Resident/${id}`);
    checkResponse(response);
}

function createResident() {

    let id = '';
    let time = new Date().getTime();
    let bodyStr = '{"firstName":"{time}_fn","lastName":"{time}_ln","apartmentNumber":"{time}_unit","email":"{time}@gmail.com"}';

    let response = http.post(`http://localhost:5132/api/Resident`, bodyStr.replaceAll('{time}', time), {
        headers: { 'Content-Type': 'application/json' },
      });
    checkResponse(response);
    if (response.status == 201) {
        id = response.json().id;
    }

    return id;
}

function checkResponse(httpResponse) {
    check(httpResponse, { 'status was Ok': (r) => r.status >= 200 && r.status < 300 });
    if (httpResponse.status >= 400) {
        errorRate.add(1);
    }
}