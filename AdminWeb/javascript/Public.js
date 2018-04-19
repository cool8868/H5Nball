function ShowModalDialogPage(url, width, height) {
    var x = parseInt(screen.width / 2.0) - (width / 2.0);
    var y = parseInt(screen.height / 2.0) - (height / 2.0);
    var isMSIE = (navigator.appName == "Microsoft Internet Explorer");
    if (isMSIE) {
        retval = window.showModalDialog(url, window, "dialogWidth:" + width + "px; dialogHeight:" + height + "px; dialogLeft:" + x + "px; dialogTop:" + y + "px; status:no; directories:yes;scrollbars:no;Resizable=no; ");
    } else {
        var win = window.open(url, "Event", "top=" + y + ",left=" + x + ",scrollbars=" + scrollbars + ",dialog=yes,modal=yes,dependance=yes,width=" + width + ",height=" + height + ",resizable=no");
        eval('try { win.resizeTo(width, height); } catch(e) { }');
        win.focus();
    }
}
