// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    const baseUrl = 'api/todo';
    const contentType = 'application/json; charset=utf-8';

    init();

    $('.todo-button-create').on('click', function () {
        var todoText = $('.todo-text').val();
        if (todoText) {
            var todoComplete = $('.todo-is-complete').is(':checked');
            var data = JSON.stringify({ text: todoText, iscomplete: todoComplete });

            ajax(baseUrl, 'POST', data, contentType, 'json', function (result) {
                populateTodoList([result]);
                $('.todo-text').removeClass('is-invalid').val('');
                $('.todo-is-complete').prop('checked', false);
            });
        } else {
            $('.todo-text').addClass('is-invalid');
        }
    });

    function init() {
        ajax(baseUrl, 'GET', null, null, null, populateTodoList);
    }

    function ajax(url, method, data, contentType, dataType, onSuccess) {
        return $.ajax({
            url: url,
            type: method,
            data: data,
            contentType: contentType,
            dataType: dataType,
            success: onSuccess
        });
    }

    function populateTodoList(todoList) {
        var table = $('#todo-table');
        jQuery.each(todoList, function (i, todo) {
            table.append(generateTodoRow(todo));
        });

        $('#todo-container').append(table);
    }

    function generateTodoRow(todo) {
        var text = $('<td>').text(todo.text);
        var isComplete = $('<td>').addClass('todo-is-complete').text(todo.isComplete ? 'Yes' : 'No');
        var removeButton = $('<button>')
            .text('Delete')
            .addClass('btn btn-danger m-2')
            .on('click', function () {
                ajax(`${baseUrl}/${todo.id}`, 'DELETE', null, null, null, function () {
                    $(`tr[data-id=${todo.id}]`).remove();
                });
            });

        var completeData = JSON.stringify({ text: todo.text, isComplete: true });
        var completeButton = $('<button>')
            .text('Complete')
            .addClass('btn btn-success')
            .on('click', function () {
                ajax(`${baseUrl}/${todo.id}`, 'PUT', completeData, contentType, 'json', function (newTodo) {
                    var newRow = generateTodoRow(newTodo);
                    var oldRow = $(`tr[data-id=${todo.id}]`);
                    newRow.insertBefore(oldRow);
                    oldRow.remove();
                });
            });

        var actions = $('<td>')
            .addClass('todo-actions')
            .append(removeButton)
            .append(completeButton);

        return $('<tr>')
            .attr('data-id', todo.id)
            .append(text)
            .append(isComplete)
            .append(actions);
    }
});
