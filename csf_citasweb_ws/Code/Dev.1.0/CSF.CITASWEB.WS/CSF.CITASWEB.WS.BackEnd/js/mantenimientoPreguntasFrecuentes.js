function ValidacionesAdicionales() {
    if ($('#placeCuerpo_txtPregunta').val().length > 500) {
        alert("El campo " + $('#placeCuerpo_txtPregunta').parent().find('label').html().trim() + " no debe tener más de 500 caracteres");
        $('#placeCuerpo_txtPregunta').focus();
        return false;
    }
    if ($('#placeCuerpo_txtRespuesta').val().length > 5000) {
        alert("El campo " + $('#placeCuerpo_txtRespuesta').parent().find('label').html().trim() + " no debe tener más de 5000 caracteres");
        $('#placeCuerpo_txtRespuesta').focus();
        return false;
    }
    return true;
}