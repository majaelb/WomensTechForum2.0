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



const myModal = new bootstrap.Modal(document.getElementById('myModal'), {});
const modalTriggerButtons = document.querySelectorAll('.modal-trigger');
//const modalUserIdElement = document.getElementById('modalUserId');
//const modalRoleNameElement = document.getElementById('modalRoleName');
const confirmDeleteButton = document.getElementById('confirmDelete');

modalTriggerButtons.forEach(button => {
    button.addEventListener('click', function () {
        const roleName = this.getAttribute('data-modal-role-name');
        const userId = this.getAttribute('data-modal-user-id');

        //console.log("Button clicked. Role name:", roleName, "User ID:", userId);

        //modalUserIdElement.textContent = userId;
        //modalRoleNameElement.textContent = roleName;

        confirmDeleteButton.href = `?RemoveUserId=${userId}&Role=${roleName}`;
        //console.log("Link updated:", confirmDeleteButton.href);

        // Öppnar modalen
        myModal.show();
    });
});


//const myPTModal = new bootstrap.Modal(document.getElementById('myPTModal'), {});
//const modalTriggerButton = document.getElementById('modalTriggerButton');
//const modalPTElement = document.getElementById('modalPTId');
//const confirmReportBtn = document.getElementById('confirmReport');

//modalTriggerButton.addEventListener('click', function () {
//    const postthreadId = this.getAttribute('data-modal-postthread-id');

//    modalPTElement.textContent = postthreadId;

//    confirmReportBtn.href = `?RemoveUserId=${postthreadId}`;

//    myPTModal.show();
//});


const myPTModal = new bootstrap.Modal(document.getElementById('myPTModal'), {});
const modalTriggerButton = document.getElementById('modal-trigger');
const modalPTElement = document.getElementById('modalPTId');
const confirmReportLink = document.getElementById('confirmReport');

modalTriggerButton.addEventListener('click', function () {
    const postthreadId = this.getAttribute('data-modal-postthread-id');
    //const updateUrl = `?changePTId=${postthreadId}`;
    //confirmReportLink.setAttribute('href', updateUrl);

    confirmReportLink.href = `?changePTId=${postthreadId}`;

    modalPTElement.textContent = postthreadId;
    myPTModal.show();
});



