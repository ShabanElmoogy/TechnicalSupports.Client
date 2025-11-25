window.methods = {
    CreateCookie: function (name, value, days) {

        var expires;
        if (days) {

            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }

        else {
            expires = "";
        }

        document.cookie = name + "=" + value + expires + "; path=/";
    },

    GetCookie: function (cname) {

        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');

        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];

            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }

            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },
    getBrowserLanguage: function() {
        return (navigator.languages && navigator.languages.length) ? navigator.languages[0] :
            navigator.userLanguage || navigator.language || navigator.browserLanguage || 'en';
    }
}
