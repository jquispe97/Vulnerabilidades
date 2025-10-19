function ValidacionesAdicionales() {
    if ($('#placeCuerpo_txtNombres').val().length > 50) {
        alert("El campo " + $('#placeCuerpo_txtNombres').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtNombres').focus();
        return false;
    }
    if ($('#placeCuerpo_txtApellidoPaterno').val().length > 50) {
        alert("El campo " + $('#placeCuerpo_txtApellidoPaterno').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtApellidoPaterno').focus();
        return false;
    }
    if ($('#placeCuerpo_txtApellidoMaterno').val().length > 50) {
        alert("El campo " + $('#placeCuerpo_txtApellidoMaterno').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtApellidoMaterno').focus();
        return false;
    }
    if ($('#placeCuerpo_txtCargo').val().length > 50) {
        alert("El campo " + $('#placeCuerpo_txtCargo').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtCargo').focus();
        return false;
    }
    if ($('#placeCuerpo_txtFoto').val().length > 100) {
        alert("El campo " + $('#placeCuerpo_txtFoto').parent().find('label').html().trim() + " no debe tener más de 100 caracteres");
        $('#placeCuerpo_txtFoto').focus();
        return false;
    }
    if ($('#placeCuerpo_txtTituloMedico').val().length > 5000) {
        alert("El campo " + $('#placeCuerpo_txtTituloMedico').parent().find('label').html().trim() + " no debe tener más de 5000 caracteres");
        $('#placeCuerpo_txtTituloMedico').focus();
        return false;
    }
    if ($('#placeCuerpo_txtPremios').val().length > 5000) {
        alert("El campo " + $('#placeCuerpo_txtPremios').parent().find('label').html().trim() + " no debe tener más de 5000 caracteres");
        $('#placeCuerpo_txtPremios').focus();
        return false;
    }
    if ($('#placeCuerpo_txtPertenenciaSociedad').val().length > 5000) {
        alert("El campo " + $('#placeCuerpo_txtPertenenciaSociedad').parent().find('label').html().trim() + " no debe tener más de 5000 caracteres");
        $('#placeCuerpo_txtPertenenciaSociedad').focus();
        return false;
    }
    if ($('#placeCuerpo_txtInvestigaciones').val().length > 5000) {
        alert("El campo " + $('#placeCuerpo_txtInvestigaciones').parent().find('label').html().trim() + " no debe tener más de 5000 caracteres");
        $('#placeCuerpo_txtInvestigaciones').focus();
        return false;
    }
    if ($('#placeCuerpo_txtRNE').val() != '' && !/^[0-9]{0,11}$/.test($('#placeCuerpo_txtRNE').val())) {
        alert("El campo " + $('#placeCuerpo_txtRNE').parent().find('label').html().trim() + " debe ser numérico");
        $('#placeCuerpo_txtRNE').focus();
        return false;
    }
    if ($('#placeCuerpo_txtIdiomas').val().length > 50) {
        alert("El campo " + $('#placeCuerpo_txtIdiomas').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtIdiomas').focus();
        return false;
    }
    if ($('#placeCuerpo_txtInformacionAdicional').val().length > 500) {
        alert("El campo " + $('#placeCuerpo_txtInformacionAdicional').parent().find('label').html().trim() + " no debe tener más de 500 caracteres");
        $('#placeCuerpo_txtInformacionAdicional').focus();
        return false;
    }
    return true;
}

function verFoto(inputURL) {
    window.open($('#' + inputURL).val().startsWith("http") ? $('#' + inputURL).val() : ("http://" + $('#' + inputURL).val()), "_blank", "resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=400, top=200, left=200");
}