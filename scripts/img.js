function closeModal(id) {
    document.getElementById(id + "-imageOverlay").style.display = "none";
    document.body.style.overflow = "auto"; // Abilita lo scrolling della pagina dopo la chiusura dell'overlay
}

function openModal(id) {
    document.getElementById(id + "-imageOverlay").style.display = "flex";
    document.body.style.overflow = "hidden"; // Impedisce lo scrolling della pagina dietro l'overlay
    timestamp = Date.now();
    document.body.addEventListener("click", function (event) {
        if (Date.now() - timestamp > 200) closeModal(id);
    });
}
