/********************************** CRUD **********************************/

/* Remove button */
function OnClickRemove(entity, id) {
    if (confirm("Confirmer la suppression?") == true) {
        $.get(entity + '/Remove/' + id, function (response) {
            setTimeout(function () { alertify.log(response); }, 200);
            $('#cat_tr_' + id).remove();
        });
    }
}

/* Add button */
/* Show the add modal view */
function OnClickAdd(entity) {
    $('#manager_contener').load(entity + '/Add', function () {
        $.validator.unobtrusive.parse('#manager_modal');
        $('#manager_modal').modal('show');
    });
}

/* Edit button */
/* Show the edit modal view */
function OnClickEdit(entity, id) {
    $('#manager_contener').load(entity + '/Edit/' + id, function () {
        $.validator.unobtrusive.parse('#manager_modal');
        $('#manager_modal').modal('show');
    });
}

/* Modal view submit button */
function OnSaveChanges(e) {
    $('#manager_modal').modal('hide');
    setTimeout(function () { alertify.log(e.msg); }, 800);

    if (e.isNew) {
        var str = '<tr id="cat_tr_' + e.id + '"><td>Nouveau</td>';

        $.each(e.fields, function (index, value) {
            str += '<td id="' + index + '_' + e.id + '">' + value + '</td>';
        });

        str += '<td class="td_btn"><button class="btn btn-success" ' +
               'onclick="OnClickEdit(\'' + e.entityName + '\', ' + e.id + ')">Editer</button></td>';

        str += '<td class="td_btn"><button class="btn btn-danger" ' +
               'onclick="OnClickRemove(\'' + e.entityName + '\', ' + e.id + ')">Supprimer</button></td>';

        $('#manager_tab tr:first').after(str);
    }
    else {
        $.each(e.fields, function (index, value) {
            $('#' + index + '_' + e.id).html(value);
        });
    }
}