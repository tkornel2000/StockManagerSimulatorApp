import axios from "axios";

// Eredeti baseURL
const dotnetApi = axios.create({
    baseURL: "https://localhost:7020/api/"
});

// Új baseURL
const pythonApi = axios.create({
    baseURL: "http://127.0.0.1:5000/"
});

export { dotnetApi, pythonApi };
