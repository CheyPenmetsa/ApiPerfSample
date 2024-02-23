import http from 'k6/http';
import { check, sleep } from 'k6';
import { Rate } from 'k6/metrics';

const MAX_VUS = `${__ENV.MAX_VUS}`;
const BASE_URL = `${__ENV.BASE_URL}`;

export let errorRate = new Rate('errors');

export let options = {
    stages: [
        { duration: '30s', target: MAX_VUS },
        { duration: '60s', target: MAX_VUS },
        { duration: '10s', target: 0 },
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
    let response = http.get(`${BASE_URL}/api/Resident/${id}`);
    checkResponse(response);
}

function createResident() {

    let id = '';
    let time = new Date().getTime();
    let bodyStr = '{"firstName":"{time}_fn","lastName":"{time}_ln","apartmentNumber":"{time}_unit","email":"{time}@gmail.com"}';

    let response = http.post(`${BASE_URL}/api/Resident`, bodyStr.replaceAll('{time}', time), {
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