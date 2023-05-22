var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl)
})



function openForm() {
    document.getElementById("myForm").style.display = "block";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
}

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

//$(document).ready(function () {
//    $('.clickable-row').click()
//})



document.getElementById("openModalLink").addEventListener("click", function (event) {
    event.preventDefault(); // Förhindra att länken följs
    var id = event.target.getAttribute("data-id"); // Hämta ID från attributet
    document.getElementById("modalId").textContent = id; // Sätt ID:t i modalen
    // Här kan du utföra övriga åtgärder med ID:t eller modalen
    console.log("Klickat på länken med ID: " + id);
});
