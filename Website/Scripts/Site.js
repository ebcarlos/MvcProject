/********************************** CRUD **********************************/

/* Remove button */
function OnClickRemove(entityName, id) {
    if (confirm("Confirmer la suppression?") == true) {
        $.get(entityName + '/Remove/' + id, function (response) {
            setTimeout(function () { alertify.log(response); }, 200);
            $('#tr_' + id).remove();
        });
    }
}

/* Add button */
/* Show the add modal view */
function OnClickAdd(entityName) {
    $('#manager_contener').load(entityName + '/Add', function () {
        $.validator.unobtrusive.parse('#manager_modal');
        $('#manager_modal').modal('show');
    });
}

/* Edit button */
/* Show the edit modal view */
function OnClickEdit(entityName, id) {
    $('#manager_contener').load(entityName + '/Edit/' + id, function () {
        $.validator.unobtrusive.parse('#manager_modal');
        $('#manager_modal').modal('show');
    });
}

/* Modal view submit button */
function OnSaveChanges(e) {
    $('#manager_modal').modal('hide');
    $('#manager_tab').show();

    setTimeout(function () { alertify.log(e.msg); }, 800);

    if (e.isNew) {
        var str = '<tr id="tr_' + e.id + '"><td>Nouveau</td>';

        $.each(e.fields, function (index, value) {
            str += '<td id="' + index + '_' + e.id + '">' + value + '</td>';
        });

        str += '<td class="td_btn">' +
               '<button class="btn btn-success" onclick="OnClickEdit(\'' + e.entityName + '\', ' + e.id + ')">Editer</button> ' +
               '<button class="btn btn-danger" onclick="OnClickRemove(\'' + e.entityName + '\', ' + e.id + ')">Supprimer</button>' +
               '</td>';

        $('#manager_tab tr:first').after(str);
    }
    else {
        $.each(e.fields, function (index, value) {
            $('#' + index + '_' + e.id).html(value);
        });
    }
}