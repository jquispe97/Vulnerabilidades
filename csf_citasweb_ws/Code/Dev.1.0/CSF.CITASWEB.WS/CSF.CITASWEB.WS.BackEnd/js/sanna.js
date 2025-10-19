/*Funciones al inicio*/
$(document).ready(function () {
    $('#menu li:has(span)').click(function () {
        if ($(this).find('ul').css('display') !== 'none') {
            $(this).find('span').css("background-image", "url('../images/iconos/expandir.png')");
            $(this).find('ul').fadeOut();
        }
        else {
            $(this).find('span').css("background-image", "url('../images/iconos/contraer.png')");
            $(this).find('ul').fadeIn();
        }
    });

    limpiarCampos();
    funcionalidadesInicialesDuplicadas();

    if (typeof EjecutarAlInicioReady == 'function') {
        EjecutarAlInicioReady();
    }
});

var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_initializeRequest(prm_InitializeRequest);
prm.add_endRequest(prm_EndRequest);

function prm_InitializeRequest(sender, args) {
    $('#divCargando').show();
}

function prm_EndRequest(sender, args) {
    $('#divCargando').fadeOut();

    funcionalidadesInicialesDuplicadas();
}



/*Funciones declaradas*/
function funcionalidadesInicialesDuplicadas() {
    $(".divInput .input").focusout(function () {
        if ($(this).val() != "" && $(this).val() != null) {
            $(this).addClass("has-content");
            if ($(this).hasClass("imagen")) {
                $(this).parent().find('.botonLink').show(1000);
            }
        } else {
            $(this).removeClass("has-content");
            if ($(this).hasClass("imagen")) {
                $(this).parent().find('.botonLink').hide(1000);
            }
        }
    });

    $(".divInput .input").each(function () {
        if ($(this).val() != '' && $(this).val() != null) {
            $(this).addClass("has-content");
            if ($(this).hasClass("imagen")) {
                $(this).parent().find('.botonLink').show(1000);
            }
        }
    });

    if (typeof EjecutarAlInicio == 'function') {
        EjecutarAlInicio();
    }
}

function limpiarCampos() {
    $('.divInput .input').val('');
    $('.divInput .input').removeClass("has-content");
    $('.botonLink').hide();
}

function validarFormulario(nombreClase) {
    var encontroError = false;
    $('.' + nombreClase).each(function () {
        if ($(this).val() == "") {
            alert("El campo " + $(this).parent().find('label').html().trim() + " es obligatorio");
            $(this).focus();
            encontroError = true
            return false;
        }
    });
    if (encontroError == false && typeof ValidacionesAdicionales == 'function') {
        return ValidacionesAdicionales();
    }
    else {
        return !encontroError;
    }
}

function MensajeDePopup(mensaje) {
    alert(mensaje);
}