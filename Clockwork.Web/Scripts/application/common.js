var apiBaseUrl = "http://localhost:50000/api/";

function ApiRequest(url, method, body, onreadystatechange) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = onreadystatechange;
    xhttp.open(method, apiBaseUrl + url, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send(body);
}

function IsApiResponseSuccess(response) {
    return response.readyState === 4 && response.status === 200;
}

function formatDateTime(value) {
    var dt = new Date(value);
    return dt.toLocaleDateString() + " " + dt.toLocaleTimeString();
}