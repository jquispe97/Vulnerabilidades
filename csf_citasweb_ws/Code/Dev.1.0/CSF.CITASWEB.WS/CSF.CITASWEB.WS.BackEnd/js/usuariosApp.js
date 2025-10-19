function EjecutarAlInicioReady() {
    var hoy = new Date();
    var dd = hoy.getDate();
    var mm = hoy.getMonth() + 1;
    var yyyy = hoy.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    hoy = yyyy + '-' + mm + '-' + dd;
    $('#placeCuerpo_txtFechaInicio').val(hoy);
    $('#placeCuerpo_txtFechaInicio').addClass("has-content");
    $('#placeCuerpo_txtFechaFin').val(hoy);
    $('#placeCuerpo_txtFechaFin').addClass("has-content");
}