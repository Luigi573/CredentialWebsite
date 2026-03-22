const studentSelectCheckmarks = document.querySelectorAll(".student-checkbox");
const deleteButton = document.getElementById("delete_students");

studentSelectCheckmarks.forEach(checkmark => {
    checkmark.addEventListener("change", toggleStudentSelection);
});

function toggleStudentSelection() {
    const anyChecked = Array.from(studentSelectCheckmarks).some(checkmark => checkmark.checked);

    deleteButton.disabled = !anyChecked;
}

let selectedStudents = new Set();
document.addEventListener("change", function (e) {
    if (e.target.matches(".student-checkbox")) {
        const checkbox = e.target;
        const tableRow = checkbox.closest("tr");
        const id = e.target.value;

        if (checkbox.checked) {
            selectedStudents.add(id);
            tableRow.classList.add("table-secondary");
        } else {
            selectedStudents.delete(id);
            tableRow.classList.remove("table-secondary");

            // Check if it matches search input; hide if not
            const searchText = searchInput.value.trim().toLowerCase();
            const name = tableRow.querySelector("td:nth-child(2)").textContent.toLowerCase();

            if (!name.includes(searchText)) {
                tableRow.style.display = "none";
            }
        }

        deleteButton.disabled = selectedStudents.size === 0;
    }
});


const searchInput = document.getElementById("search_input");
const semesterSelect = document.getElementById("semester_select");
searchInput.addEventListener("input", filterStudents);
semesterSelect.addEventListener("change", filterStudents);

function filterStudents() {
    const searchText = searchInput.value.trim().toLowerCase();
    const semesterValue = semesterSelect.value;

    document.querySelectorAll("#students_table tbody tr").forEach(row => {
        const name = row.querySelector("td:nth-child(2)").textContent.trim().toLowerCase() + " " + row.querySelector("td:nth-child(3)").textContent.trim().toLowerCase();
        const semester = row.querySelector("td:nth-child(5)").textContent.trim();
        const checkbox = row.querySelector(".student-checkbox");
        const id = checkbox.value;
        const isSelected = selectedStudents.has(id);

        const matchesSearch = searchText === "" || name.includes(searchText);
        const matchesSemester = semesterValue === "" || semester === semesterValue;

        row.style.display = (matchesSearch && matchesSemester) || isSelected ? "" : "none";
    });
}

const schoolSelect = document.getElementById("school_select");
schoolSelect.addEventListener("change", function () {
    const searchForm = document.getElementById("search_form");
    searchForm.submit();
});