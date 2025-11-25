window.consoleLog = function (message) {
    console.log(message);
};

window.copyToClipboard = (text) => {
    navigator.clipboard.writeText(text).then(() => {
        console.log('Text copied to clipboard');
    }).catch((err) => {
        console.error('Failed to copy text: ', err);
    });
};

window.getCardBodyText = (element) => {
    if (!element) return "";
    const textContent = Array.from(element.querySelectorAll(".RadzenText")).map(el => el.textContent).join("\n");
    return textContent;
};

function downloadFileFromStream(fileName, contentStreamReference) {
    const blob = new Blob([contentStreamReference], { type: 'application/octet-stream' });
    const url = URL.createObjectURL(blob);

    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName || 'report';
    document.body.appendChild(anchor);
    anchor.click();

    // Cleanup
    URL.revokeObjectURL(url);
    document.body.removeChild(anchor);
}

function setIframeSrc(iframeId, url) {
    const iframe = document.getElementById(iframeId);
    if (iframe) {
        iframe.src = url;
    }
}

function loadReportInIframe(reportBytes) {
    // Create a Blob from the byte array (PDF data)
    const blob = new Blob([new Uint8Array(reportBytes)], { type: 'application/pdf' });

    // Create an object URL from the Blob
    const url = URL.createObjectURL(blob);

    // Set the iframe src to the Blob URL
    const iframe = document.getElementById("reportIframe");
    iframe.src = url;
}

function GetCurrentTheme() {
    const storedTheme = localStorage.getItem("Theme");
    return storedTheme ? storedTheme : "Light";
}

function setKeyboardLanguage(language) {
    document.documentElement.lang = language;
}

async function clearApplicationStorage() {
    try {
        // Clear localStorage and sessionStorage
        localStorage.clear();
        sessionStorage.clear();

        // Clear Service Worker Cache
        if (caches) {
            const cacheNames = await caches.keys();

            for (const name of cacheNames) {
                await caches.delete(name);
            }
        } else {
            console.warn("Cache Storage is not supported in this browser.");
        }

    } catch (error) {

    }
}