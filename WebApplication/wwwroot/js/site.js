window.deleteUser = function deleteUser() {
    return confirm("Are you sure you want to delete this user?");
}

const fileInput = document.getElementById('photo_uploader');
const photoPreview = document.getElementById('photo_preview');

fileInput.addEventListener('change', displayPhoto);

function displayPhoto() {

    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();
        reader.onload = function (e) {
            photoPreview.src = e.target.result;
        };
        reader.readAsDataURL(fileInput.files[0]);
    } else {
        photoPreview.src = '/photos/placeholder.jpg';
    }
}

const submitButton = document.getElementById('submit_button');
submitButton.addEventListener('click', submitStudent);

function submitStudent() {

}