function ValidacionesAdicionales() {
    if ($('#placeCuerpo_txtNombre').val().length > 100) {
        alert("El campo " + $('#placeCuerpo_txtNombre').parent().find('label').html().trim() + " no debe tener más de 100 caracteres");
        $('#placeCuerpo_txtNombre').focus();
        return false;
    }
    if ($('#placeCuerpo_txtRUC').val() != '' && !/^[0-9]{11}$/.test($('#placeCuerpo_txtRUC').val())) {
        alert("El campo " + $('#placeCuerpo_txtRUC').parent().find('label').html().trim() + " debe ser numérico de 11 dígitos");
        $('#placeCuerpo_txtRUC').focus();
        return false;
    }
    if ($('#placeCuerpo_txtRUCSunasa').val() != '' && !/^[0-9]{11}$/.test($('#placeCuerpo_txtRUCSunasa').val())) {
        alert("El campo " + $('#placeCuerpo_txtRUCSunasa').parent().find('label').html().trim() + " debe ser numérico de 11 dígitos");
        $('#placeCuerpo_txtRUCSunasa').focus();
        return false;
    }
    if ($('#placeCuerpo_txtCodigoSunasa').val().length > 15) {
        alert("El campo " + $('#placeCuerpo_txtCodigoSunasa').parent().find('label').html().trim() + " no debe tener más de 15 caracteres");
        $('#placeCuerpo_txtCodigoSunasa').focus();
        return false;
    }
    if ($('#placeCuerpo_txtDireccion').val().length > 500) {
        alert("El campo " + $('#placeCuerpo_txtDireccion').parent().find('label').html().trim() + " no debe tener más de 500 caracteres");
        $('#placeCuerpo_txtDireccion').focus();
        return false;
    }
    if ($('#placeCuerpo_txtCiudad').val().length > 50) {
        alert("El campo " + $('#placeCuerpo_txtCiudad').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtCiudad').focus();
        return false;
    }
    if ($('#placeCuerpo_txtFoto').val().length > 100) {
        alert("El campo " + $('#placeCuerpo_txtFoto').parent().find('label').html().trim() + " no debe tener más de 100 caracteres");
        $('#placeCuerpo_txtFoto').focus();
        return false;
    }
    if ($('#placeCuerpo_txtAbreviatura').val().length > 5) {
        alert("El campo " + $('#placeCuerpo_txtAbreviatura').parent().find('label').html().trim() + " no debe tener más de 5 caracteres");
        $('#placeCuerpo_txtAbreviatura').focus();
        return false;
    }
    if ($('#placeCuerpo_txtHorariosAtencion').val().length > 500) {
        alert("El campo " + $('#placeCuerpo_txtHorariosAtencion').parent().find('label').html().trim() + " no debe tener más de 500 caracteres");
        $('#placeCuerpo_txtHorariosAtencion').focus();
        return false;
    }
    if ($('#placeCuerpo_txtTelefono').val().length > 15) {
        alert("El campo " + $('#placeCuerpo_txtTelefono').parent().find('label').html().trim() + " no debe tener más de 15 caracteres");
        $('#placeCuerpo_txtTelefono').focus();
        return false;
    }
    if (!/^(\-)?[0-9]{1,8}(\.[0-9]{0,10})?$/.test($('#placeCuerpo_txtLatitud').val())) {
        alert("El campo " + $('#placeCuerpo_txtLatitud').parent().find('label').html().trim() + " debe ser numérico (max: 8 enteros, 10 decimales)");
        $('#placeCuerpo_txtLatitud').focus();
        return false;
    }
    if (!/^(\-)?[0-9]{1,8}(\.[0-9]{0,10})?$/.test($('#placeCuerpo_txtLongitud').val())) {
        alert("El campo " + $('#placeCuerpo_txtLongitud').parent().find('label').html().trim() + " debe ser numérico (max: 8 enteros, 10 decimales)");
        $('#placeCuerpo_txtLongitud').focus();
        return false;
    }
    return true;
}

function verFoto(inputURL) {
    window.open($('#' + inputURL).val().startsWith("http") ? $('#' + inputURL).val() : ("http://" + $('#' + inputURL).val()), "_blank", "resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=400, top=200, left=200");
}