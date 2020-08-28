function validaLot(src, valor, control) {
    var msg = '';

    // Llamada a metodo de pagina
    if (valor.toString() != '') {
        PageMethods.validaExistLot(src, valor, CallSuccess, CallFailed);
        if (msg != '')
        {var t = 0;}
    }
    return;
}

// Operacion Completa
function CallSuccess(res, destCtrl) {
 
    return true;
}

// Error
function CallFailed(res, destCtrl) {
    //var control = document.getElementById(destCtrl);
    //control.value = '';
    alert(res.get_message());
    return false;
}