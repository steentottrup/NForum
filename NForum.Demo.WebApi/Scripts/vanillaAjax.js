var ajax = {};

ajax.configure = function (settings) {
	if (settings) {
		if (settings.onUnauthorized) {
			ajax.onUnauthorized = settings.onUnauthorized;
		}
	}
};

ajax.send = function (url, callback, method, data, sync) {
	var request = new XMLHttpRequest();
	request.open(method, url, sync);
	request.onreadystatechange = function () {
		if (request.readyState == XMLHttpRequest.DONE) {
			if (request.status === 200) {
				callback(JSON.parse(request.response));
			}
			else if (request.status === 401) {
				if (ajax.onUnauthorized) {
					ajax.onUnauthorized(request);
				}
			}
			else {
				console.log(request.status);
			}
		}
	};
	request.setRequestHeader("Accept", "application/json");
	request.setRequestHeader("Content-Type", "application/json");
	if (method == "POST") {
		request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
	}
	request.send(data)
};

ajax.get = function (url, data, callback) {
	var query = [];
	for (var key in data) {
		query.push(encodeURIComponent(key) + "=" + encodeURIComponent(data[key]));
	}
	ajax.send(url + (query ? "?" : "") + query.join("&"), callback, "GET", null, false)
};

ajax.post = function (url, data, callback) {
	var query = [];
	for (var key in data) {
		query.push(encodeURIComponent(key) + "=" + encodeURIComponent(data[key]));
	}
	ajax.send(url, callback, "POST", query.join("&"), false)
};

ajax.put = function (url, data, callback) {
	var query = [];
	for (var key in data) {
		query.push(encodeURIComponent(key) + "=" + encodeURIComponent(data[key]));
	}
	ajax.send(url, callback, "PUT", query.join("&"), false)
};

ajax.delete = function (url, callback) {
	ajax.send(url, callback, "DELETE", null, false)
};
