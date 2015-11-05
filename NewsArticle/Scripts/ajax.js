var ajaxOptions = {
    defaultAjaxTimeout: 600000,
    exceptionStart: '<!--',
    exceptionEnd: '-->',
};

var ajax = {
    sendRequest: function (type, dataType, url, data, success, error, opName, svcName) {
        var hubUxUrl = url;
        var hubUxData = data;
        if (url.toLowerCase().indexOf("http://") == 0 || url.toLowerCase().indexOf("https://") == 0) {
            hubUxUrl = "/Iridium/HttpPostProxy";
            var serializedData = "";
            if (data) {
                for (var prop in data) {
                    serializedData = serializedData + "&" + prop + "=" + encodeURIComponent(data[prop]);
                }
                if (serializedData.length > 1) {
                    serializedData = serializedData.substring(1);
                }
            }
            hubUxData =
                {
                    url: url,
                    data: serializedData
                };
        }
        if (!svcName) {
            svcName = "IridiumUX";
        }

        //The 'service' object is embedded within 'window' as part of the updated WebI code
        window["service"][type.toLowerCase()](requestOptions =
                {
                    type: type,
                    dataType: dataType,
                    url: hubUxUrl,
                    targetUri: hubUxUrl,
                    data: hubUxData,
                    timeout: ajaxOptions.defaultAjaxTimeout,
                    serviceName: svcName,
                    currentOperationName: hubUxUrl
                }).done(function (result) {
                    if (success) success(result);
                }).fail(function (jqXhr, textStatus, errorThrown) {
                    if (error && jqXhr) {
                        var responseText = jqXhr.responseText;
                        // responseText can be NaN if we get request timeout
                        if (!responseText) {
                            responseText = "";
                        }
                        var index = responseText.indexOf(ajaxOptions.exceptionStart);
                        if (index > 0) {
                            var exception = responseText.substr(index + ajaxOptions.exceptionStart.length + 1);
                            index = exception.lastIndexOf(ajaxOptions.exceptionEnd);
                            if (index > 0) {
                                exception = exception.substr(0, index);
                            }
                            error(exception);
                        } else {
                            error(errorThrown);
                        }
                    }
                });
    },

    postJson: function (url, data, success, error, operationName, serviceName) {
        ajax.sendRequest("POST", "json", url, data, success, error, operationName, serviceName);
    },

    getJson: function (url, data, success, error, operationName, serviceName) {
        ajax.sendRequest("GET", "json", url, data, success, error, operationName, serviceName);
    },

    post: function (url, data, success, error, operationName, serviceName) {
        ajax.sendRequest("POST", "text", url, data, success, error, operationName, serviceName);
    }
}