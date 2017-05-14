var ServiceModel = function(code, ver, url, type, isAuth) {
    this.code = code;
    this.ver = ver;
    this.url = url;
    this.type = type;
    this.isAuth = isAuth;
}

var config = {
    DefaultUrl: "",
    Mode: "dev",
    ServiceTable: [
        new ServiceModel('00000000', "1.0", "http://localhost:5000/api/account/GetVerficationCode", 'GET', false)
    ]
}
export default config;