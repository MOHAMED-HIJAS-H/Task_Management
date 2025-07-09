// team-collab.js
$(document).ready(function () {
    $('#assignForm').on('submit', function (e) {
        e.preventDefault();

        const member = $('#memberName').val().trim();
        const task = $('#taskName').val().trim();

        if (member !== '' && task !== '') {
            const taskHtml = `<div class="task-item">
                <strong>${member}</strong>
                <span>Task: ${task}</span>
            </div>`;

            $('#taskList').append(taskHtml);

            // Reset fields
            $('#memberName').val('');
            $('#taskName').val('');
        }
    });
});
