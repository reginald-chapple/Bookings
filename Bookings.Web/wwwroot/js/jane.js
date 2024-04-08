var jane = (function() {
    var baseUrl = 'http://localhost:5232/'; // Set your base URL here

    function ajax(method, url, data, callback) {
        var xhr = new XMLHttpRequest();
        xhr.open(method, url, true);
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.onreadystatechange = function() {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status >= 200 && xhr.status < 400) {
                    callback(null, JSON.parse(xhr.responseText));
                } else {
                    callback(new Error('Request failed with status: ' + xhr.status));
                }
            }
        };

        xhr.onerror = function() {
            callback(new Error('Request failed'));
        };

        if (data) {
            xhr.send(JSON.stringify(data));
        } else {
            xhr.send();
        }
    }

    return {
        create: function(resource, data, callback) {
            ajax('POST', baseUrl + '/' + resource, data, callback);
        },

        read: function(resource, callback) {
            ajax('GET', baseUrl + '/' + resource, null, callback);
        },

        update: function(resource, id, data, callback) {
            ajax('PUT', baseUrl + '/' + resource + '/' + id, data, callback);
        },

        del: function(resource, id, callback) {
            ajax('DELETE', baseUrl + '/' + resource + '/' + id, null, callback);
        }
    };
})();
