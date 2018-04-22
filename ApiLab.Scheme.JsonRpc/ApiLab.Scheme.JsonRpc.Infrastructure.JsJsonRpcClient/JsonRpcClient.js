
this.JsonRpcClient = {

    build: function(url) {
        var jrpc = new simple_jsonrpc();

        jrpc.toStream = function(_msg) {
            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (this.readyState != 4) return;
                jrpc.messageHandler(this.responseText);
            };
            xhr.open("POST", url, true);
            xhr.setRequestHeader('Content-Type', 'application/json-rpc');
            xhr.send(_msg);
        };

        return new Proxy({}, {
            get: function(target, prop) {
                return function() {
                    return jrpc.call(prop, [].slice.call(arguments));
                };
            },
        });
    },

};
