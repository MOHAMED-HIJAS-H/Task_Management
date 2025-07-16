const taskManagerContainer = document.querySelector(".taskManager");
const confirmEl = document.querySelector(".confirm");
const confirmedBtn = confirmEl.querySelector(".confirmed");
const cancelledBtn = confirmEl.querySelector(".cancel");
let taskIdToBeDeleted = null; // Use DB task ID now

$(document).ready(function () {

    // Alert and submit task
    $('#taskForm').submit(function (event) {
        event.preventDefault();
        const taskInput = $('#taskInput').val().trim();
        const taskDeadline = $('#taskDeadline').val();

        if (taskInput !== '' && taskDeadline !== '') {

            const taskData = {
                title: taskInput,
                deadline: taskDeadline
            };


            $.ajax({
                url: "/home/task",
                type: "POST",
                data: JSON.stringify(taskData),
                contentType: "application/json",
                success: function () {
                    window.location.reload();
                },
                error: function (xhr) {
                    console.error("Error:", xhr.responseText);
                    //alert("Error adding task: " + xhr.responseText);
                }
            });
        }
    });

    // 🔍 Simple Client-Side Search Logic
    $('#searchForm').submit(function (e) {
        e.preventDefault();

        const keyword = $('#taskSearch').val().toLowerCase().trim();

        $('.taskCard').each(function () {
            const title = $(this).find('p:first').text().toLowerCase();

            if (title.includes(keyword)) {
                $(this).show();  // Show matching task
            } else {
                $(this).hide();  // Hide non-matching task
            }
        });
    });

    // Toggle task completion
    $('.toggleBtn').on('click', function () {
        const id = $(this).closest('.taskCard').data('index'); // ❗ now use data-id not index

        $.ajax({
            url: `/Home/Toggle`,
            type: "POST",
            data: {id},
            success: function () {
                window.location.reload();
            },
            error: function () {
                alert("Error toggling task.");
            }
        });
    });

    // Delete button click
    $('.deleteBtn').on('click', function () {
        taskIdToBeDeleted = $(this).closest('.taskCard').data('index'); // ❗ now use data-index
        confirmEl.style.display = "block";
        taskManagerContainer.classList.add("overlay");
    });

    // Confirm deletion
    confirmedBtn.addEventListener("click", () => {
        $.ajax({
            url: `/home/delete/${taskIdToBeDeleted}`,
            type: "POST",
            success: function () {
                confirmEl.style.display = "none";
                taskManagerContainer.classList.remove("overlay");
                window.location.reload();
            },
            error: function () {
                alert("Error deleting task.");
            }
        });
    });

    // Cancel deletion
    cancelledBtn.addEventListener("click", () => {
        confirmEl.style.display = "none";
        taskManagerContainer.classList.remove("overlay");
    });












    //to submit the form without page reload in ContactForm
        //$('#contactForm').submit(function (e) {
        //    e.preventDefault(); // Stop full page reload

        //    // Get form values
        //    const contact = {
        //        Name: $('input[name="Name"]').val(),
        //        Email: $('input[name="Email"]').val(),
        //        Message: $('textarea[name="Message"]').val()
        //    };

        //    // AJAX call to Home/ContactSubmit
        //    $.ajax({
        //        url: '/Home/SubmitContact',
        //        type: 'POST',
        //        data: JSON.stringify(contact),
        //        contentType: 'application/json',
        //        success: function (res) {
        //            $('#contactResponse')
        //                .removeClass('error success')
        //                .addClass(res.success ? 'success' : 'error')
        //                .text(res.message)
        //                .fadeIn();

        //            if (res.success) {
        //                $('#contactForm')[0].reset();
        //            }

        //            // Auto-hide after 5 seconds
        //            setTimeout(() => {
        //                $('#contactResponse').fadeOut();
        //            }, 5000);
        //        },
        //        error: function () {
        //            $('#contactResponse')
        //                .removeClass('success')
        //                .addClass('error')
        //                .text('Something went wrong.')
        //                .fadeIn();
        //        }
        //    });
        //});
    });






























// Load tasks from local storage or use an empty array

// Backup code for js --- Retrieve tasks from local storage or initialize an empty array 
//let tasks = JSON.parse(localStorage.getItem('tasks')) || [];

//const taskManagerContainer = document.querySelector(".taskManager");
//const confirmEl = document.querySelector(".confirm");
//const confirmedBtn = confirmEl.querySelector(".confirmed");
//const cancelledBtn = confirmEl.querySelector(".cancel");
//let indexToBeDeleted = null

//// Add event listener to the form submit event
//document.getElementById('taskForm').addEventListener('submit', handleFormSubmit);

//// Function to handle form submission
//function handleFormSubmit(event) {
//    event.preventDefault();
//    const taskInput = document.getElementById('taskInput');
//    const taskText = taskInput.value.trim();

//    if (taskText !== '') {
//        const newTask = {
//            text: taskText,
//            completed: false
//        };

//        tasks.push(newTask);
//        saveTasks();
//        taskInput.value = '';
//        renderTasks();
//    }
//}

//// Function to save tasks to local storage
//function saveTasks() {
//    localStorage.setItem('tasks', JSON.stringify(tasks));
//}

//// Initial rendering of tasks
//renderTasks();


//// Function to render tasks
//function renderTasks() {
//    const taskContainer = document.getElementById('taskContainer');
//    taskContainer.innerHTML = '';

//    tasks.forEach((task, index) => {
//        const taskCard = document.createElement('div');
//        taskCard.classList.add('taskCard');
//        let classVal = "pending";
//        let textVal = "Pending"
//        if (task.completed) {
//            classVal = "completed";
//            textVal = "Completed";
//        }
//        taskCard.classList.add(classVal);

//        const taskText = document.createElement('p');
//        taskText.innerText = task.text;

//        const taskStatus = document.createElement('p');
//        taskStatus.classList.add('status');
//        taskStatus.innerText = textVal;

//        const toggleButton = document.createElement('button');
//        toggleButton.classList.add("button-box");
//        const btnContentEl = document.createElement("span");
//        btnContentEl.classList.add("green");
//        btnContentEl.innerText = task.completed ? 'Mark as Pending' : 'Mark as Completed';
//        toggleButton.appendChild(btnContentEl);
//        toggleButton.addEventListener('click', () => {
//            tasks[index].completed = !tasks[index].completed;
//            saveTasks();
//            renderTasks();
//        });

//        const deleteButton = document.createElement('button');
//        deleteButton.classList.add("button-box");
//        const delBtnContentEl = document.createElement("span");
//        delBtnContentEl.classList.add("red");
//        delBtnContentEl.innerText = 'Delete';
//        deleteButton.appendChild(delBtnContentEl);
//        deleteButton.addEventListener('click', () => {
//            indexToBeDeleted = index
//            confirmEl.style.display = "block";
//            taskManagerContainer.classList.add("overlay");
//        });

//        taskCard.appendChild(taskText);
//        taskCard.appendChild(taskStatus);
//        taskCard.appendChild(toggleButton);
//        taskCard.appendChild(deleteButton);

//        taskContainer.appendChild(taskCard);
//    });
//}

//// function to delete the selected task
//function deleteTask(index) {
//    tasks.splice(index, 1);
//    saveTasks();
//    renderTasks();
//}

//confirmedBtn.addEventListener("click", () => {
//    confirmEl.style.display = "none";
//    taskManagerContainer.classList.remove("overlay");
//    deleteTask(indexToBeDeleted)
//});

//cancelledBtn.addEventListener("click", () => {
//    confirmEl.style.display = "none";
//    taskManagerContainer.classList.remove("overlay");
//});
