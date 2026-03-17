// wwwroot/js/shell-navigation.js
window.listenMfeNavigation = function (dotnetRef) {
    // Limpiar listener anterior si existe
    if (window._mfeMessageHandler) {
        window.removeEventListener('message', window._mfeMessageHandler);
    }

    window._mfeMessageHandler = function (event) {
        if (event.data && event.data.type === 'mfe-navigation') {
            console.log('[Shell] Mensaje recibido:', event.data.path);

            // El Shell guarda en SU sessionStorage
            sessionStorage.setItem('mfe_last_path', path);

            dotnetRef.invokeMethodAsync('OnMfeNavigated', event.data.path);
        }
    };

    window.addEventListener('message', window._mfeMessageHandler);
    console.log('[Shell] listener registrado');
};

window.getLastMfePath = function () {
    var path = sessionStorage.getItem('mfe_last_path') || '';
    console.log('[Shell] getLastMfePath:', path);
    return path;
};

window.clearLastMfePath = function () {
    sessionStorage.removeItem('mfe_last_path');
};