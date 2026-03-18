const fileInput = document.getElementById('photo_uploader');
const photoPreview = document.getElementById('photo_preview');

fileInput.addEventListener('change', DisplayPhoto);

function DisplayPhoto() {

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