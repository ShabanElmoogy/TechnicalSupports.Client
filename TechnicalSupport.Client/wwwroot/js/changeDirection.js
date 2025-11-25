// JavaScript function to set RTL direction and load RTL Bootstrap
function setRTLAndLoadRTLBootstrap() {
    //var element = document.getElementById('html');
    //if (element) {
    //    element.setAttribute('dir', 'rtl');
    //}
    document.documentElement.setAttribute('dir', 'rtl');
    var link = document.createElement('link');
    link.rel = 'stylesheet';
    link.href = 'css/bootstrap/bootstrap.rtl.min.css';
    document.head.appendChild(link);
    document.documentElement.dir = 'rtl';
}

// JavaScript function to set LTR direction
function setLTRAndLoadLTRBootstrap() {
    document.documentElement.setAttribute('dir', 'ltr');
    var link = document.createElement('link');
    link.rel = 'stylesheet';
    link.href = 'css/bootstrap/bootstrap.min.css';
    document.head.appendChild(link);
    document.documentElement.dir = 'ltr';
}

function navigateTo(url) {
    history.replaceState(null, '', url);
    window.location.href = url;
}
