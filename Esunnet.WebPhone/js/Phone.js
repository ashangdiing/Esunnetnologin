
var OcxPhone = function (e) {
    var connection, user = { AgentId: "", Ext: "", Password: null }, self = this;
    $(window).unload(function () { if (connection) connection.stop(); });
    this.event = {
        logined: null,
        error: null,
        agentStatus: null
    };
    this.callback = null;
    this.event = $.extend(this.event, e);
    this.start = function (callback) {
        connection = $.connection('phone');
        connection.error(function (d) { alert("错误消息：" + d); });
        connection.received(function (data) {
            switch (data.fun) {
                case "callback": if (self.callback) { self.callback(data.p == "True"); } break;
                case "AgentLogin": if (self.event.logined) { self.event.logined(data.p[0], data.p[1], data.p[2]); } break;
                case "Error": if (self.event.error) { self.event.error(data.p[0]); } break;
                case "AgentStatus": if (self.event.agentStatus) { self.event.agentStatus(data.p[0], data.p[1], data.p[2], data.p[3], data.p[4], data.p[5]); } break;
            }
        });
        connection.start(function () {
            if (callback) { callback(); }
        });
    }
    this.login = function (u, callback) {
        user = $.extend(user, u);
        var p = {};
        p.fun = "Login";
        p.p = [user.AgentId, "", user.Ext, user.Password || user.AgentId];
        self.callback = callback;
        connection.send(JSON.stringify(p));
    }
    this.setAgentStatus = function (stat, msg, callback) {
        var p = {};
        p.fun = "SetAgentStatus";
        p.p = [user.AgentId, stat, msg];
        self.callback = callback;
        connection.send(JSON.stringify(p));
    }
}