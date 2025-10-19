function ValidacionesAdicionales() {
    if (!/^[0-9]{11}$/.test($('#placeCuerpo_txtRUCSeguro').val())) {
        alert("El campo " + $('#placeCuerpo_txtRUCSeguro').parent().find('label').html().trim() + " debe ser numérico de 11 dígitos");
        $('#placeCuerpo_txtRUCSeguro').focus();
        return false;
    }
    if ($('#placeCuerpo_txtRazonSocial').val().length > 50) {
        alert("El campo  " + $('#placeCuerpo_txtRazonSocial').parent().find('label').html().trim() + " no debe tener más de 50 caracteres");
        $('#placeCuerpo_txtRazonSocial').focus();
        return false;
    }
    if (!/^[0-9]+$/.test($('#placeCuerpo_txtOrden').val())) {
        alert("El campo " + $('#placeCuerpo_txtOrden').parent().find('label').html().trim() + " debe ser numérico");
        $('#placeCuerpo_txtOrden').focus();
        return false;
    }
    return true;
}