function resetScrollTops() {
    // Select all elements with the 'scroll-reset' class
    const elements = document.querySelectorAll('.scroll-reset');
    elements.forEach(element => {
        if (element) {
            element.scrollTop = 0; // Reset scroll position
        }
    });
}
