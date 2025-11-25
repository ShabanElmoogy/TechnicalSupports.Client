OnLoadTheme();

function OnLoadTheme() {
    const storedTheme = localStorage.getItem("Theme");

    if (storedTheme) {
        setTheme(storedTheme);
    } else {
        setTheme("Light");
        localStorage.setItem("Theme", "Light");
    }
}
function getCurrentTheme() {
    const storedTheme = localStorage.getItem("Theme");

    if (storedTheme) {
        setTheme(storedTheme);
        return storedTheme;
    } else {
        setTheme("Light");
        localStorage.setItem("Theme", "Light");
        return "Light";
    }
}

function onClickLightDark(theme) {
    setTheme(theme);
    localStorage.setItem("Theme", theme);
}

function setTheme(theme) {
    const lightThemeLink = document.getElementById("lightThemeLink");
    const darkThemeLink = document.getElementById("darkThemeLink");

    if (theme === "Light") {
        lightThemeLink.rel = "stylesheet";
        darkThemeLink.rel = "";
    } else {
        lightThemeLink.rel = "";
        darkThemeLink.rel = "stylesheet";
    }
}