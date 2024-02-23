FROM loadimpact/k6:latest
COPY PerfShared/k6/scripts /scripts
WORKDIR /scripts
ENTRYPOINT ["k6"]
CMD ["run", "load-test.js"]