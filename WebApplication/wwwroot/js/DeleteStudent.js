const studentSelectCheckmarks = document.querySelectorAll(".student-checkbox");
const deleteButton = document.getElementById("delete_students");

studentSelectCheckmarks.forEach(checkmark => {
    checkmark.addEventListener("change", toggleStudentSelection);
});

function toggleStudentSelection() {
    const anyChecked = Array.from(studentSelectCheckmarks).some(checkmark => checkmark.checked);

    deleteButton.disabled = !anyChecked;
}