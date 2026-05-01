const modal = document.getElementById("modal");
const modalBox = document.getElementById("modal-box-contents");

function showModal () {
    modal.style.display = "flex";
}

function showSuccessModal (message) {
    modalBox.innerHTML = `<div class="text-m full">
        <div class="text-m full bold-700 success">Success</div>
        <div class="text-xxxs pd-05 mb-1">${message}</div>
    </div>`;
    modal.style.display = "flex";
}

function showFailedModal (message) {
    modalBox.innerHTML = `<div class="text-m full">
        <div class="text-m full bold-700 failed">Failed</div>
        <div class="text-xxxs pd-05 mb-1">${message}</div>
    </div>`;
    modal.style.display = "flex";
}

function closeModal () {
    modal.style.display = "none";
}