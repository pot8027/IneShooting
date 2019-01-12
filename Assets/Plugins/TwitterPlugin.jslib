var TwitterPlugin = {

    OpenNewWindow:  function(openUrl) {
        var url = Pointer_stringify(openUrl);
        window.open(url, "TweetWindow");
    }
};

mergeInto(LibraryManager.library, TwitterPlugin);