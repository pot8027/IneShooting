var TwitterPlugin = {

    OpenNewWindow:  function(openUrl) {
        var url = Pointer_stringify(openUrl);
        document.onmouseup = function()
        {
            window.open(url);
            document.onmouseup = null;
        }
        console.log("aaa");
    }
};

mergeInto(LibraryManager.library, TwitterPlugin);