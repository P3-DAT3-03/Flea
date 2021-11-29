(function () {
    function Expose(functions) {
        for (let func in functions) {
            window[func] = functions[func];
        }
    }
    
    /**
     * @type {HTMLCanvasElement}
     */
    let canvas;

    /**
     * @type {ResizeObserver}
     */
    let observer;

    let dotNetHelper;
    
    function BindDotNetInstance(_dotNetHelper) {
        dotNetHelper = _dotNetHelper;
    }
    
    /**
     * Links the size parameters of a canvas to
     * the actual size of its parent element.
     * @param selector {string} - A css selector that is used to find the canvas.
     */
    function LinkCanvasToParentSize(selector) {
        canvas = document.querySelector(selector);
        
        if (canvas == null || !(canvas instanceof HTMLCanvasElement))
            throw new Error(`No canvas found using given selector: "${selector}"`);

        // Ensures that the canvas is a block. Canvas defaults to inline
        // which can cause infinite size expansion with the resize observer.
        if (canvas.style.display !== "block")
            canvas.style.display = "block";
        
        observer = new ResizeObserver(OnResize);
        observer.observe(canvas.parentElement);
        OnResize();
    }

    function OnResize() {
        canvas.width = canvas.parentElement.clientWidth;
        canvas.height = canvas.parentElement.clientHeight;
        dotNetHelper.invokeMethodAsync('OnCanvasResize', canvas.width, canvas.height);
    }
    
    
    // Export functions
    Expose({
        BindDotNetInstance,
        LinkCanvasToParentSize,
    });
})(); 