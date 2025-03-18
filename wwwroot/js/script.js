// confirm delete
function confirmDelete() {
    return confirm("Are you sure you want to delete this album? This action cannot be undone");
}

function confirmInsert() {
    return confirm("Are you sure you want to add a new album?");
}

function confirmUpdate() {
    return confirm("Are you sure you want to confirm the updates?");
}

// show hide add album form

const addIcon = document.getElementById("add-new-album");
const hideIcon = document.getElementById("hide-new-album");
const form = document.querySelector(".add-album");

addIcon.addEventListener("click", function () {
    form.style.display = "flex";
    addIcon.style.display = "none";
    hideIcon.style.display = "inline";
});

hideIcon.addEventListener("click", function () {
    form.style.display = "none";
    hideIcon.style.display = "none";
    addIcon.style.display = "inline";
});


// radial overlay for landing page







