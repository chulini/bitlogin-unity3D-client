(function() {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        // some code..
        return;
    }

    // let canvasElement = document.getElementById("#canvas");


    // console.log(canvasElement);
    window.onresize = scaleToFit;

    function scaleToFit(e) {
        let w = 1920;
        let h = 1080;
        let innerWidth = window.innerWidth;
        let innerHeight = window.innerHeight;
        console.log(e);
        // let canvasElement = document.getElementById("#canvas");
        let gameContainer = document.getElementById("gameContainer");
        // get the scale
        var scale = Math.min(innerWidth / w, innerHeight / h);
        // get the top left position of the image
        var x = (innerWidth / 2) - (w / 2) * scale;
        var y = (innerHeight / 2) - (w / 2) * scale;

        gameContainer.style.width = (w * scale) + "px";
        gameContainer.style.height = (h * scale) + "px";
        // gameContainer.setAttribute('width', w * scale);
        // gameContainer.setAttribute('height', h * scale);

        //     console.log("resize");
        //     // canvasElement.drawImage(img, x, y, img.width * scale, img.height * scale);
    }

    scaleToFit(null);

    // const q = (selector) => document.querySelector(selector);

    // const gameContainer = q('#gameContainer');

    // const initialDimensions = {width: parseInt(gameContainer.style.width, 10), height: parseInt(gameContainer.style.height, 10)};
    // gameContainer.style.width = '100%';
    // gameContainer.style.height = '100%';

    // let gCanvasElement = null;

    // const getCanvasFromMutationsList = (mutationsList) => {
    //     for (let mutationItem of mutationsList){
    //         for (let addedNode of mutationItem.addedNodes){
    //             if (addedNode.id === '#canvas'){
    //                 return addedNode;
    //             }
    //         }
    //     }
    //     return null;
    // }

    // const setDimensions = () => {


    //     gameContainer.style.position = 'absolute';
    //     gCanvasElement.style.display = 'none';
    //     var winW = parseInt(window.getComputedStyle(gameContainer).width, 10);
    //     var winH = parseInt(window.getComputedStyle(gameContainer).height, 10);
    //     var scale = Math.min(winW / initialDimensions.width, winH / initialDimensions.height);
    //     gCanvasElement.style.display = '';
    //     gCanvasElement.style.width = 'auto';
    //     gCanvasElement.style.height = 'auto';

    //     var fitW = Math.round(initialDimensions.width * scale * 100) / 100;
    //     var fitH = Math.round(initialDimensions.height * scale * 100) / 100;
    //     console.log(fitW + " x " +fitH);
    //     gCanvasElement.setAttribute('width', fitW);
    //     gCanvasElement.setAttribute('height', fitH);
    // }

    // window.setDimensions = setDimensions;

    // const registerCanvasWatcher = () => {
    //     let debounceTimeout = null;
    //     const debouncedSetDimensions = () => {
    //         if (debounceTimeout !== null) {
    //             clearTimeout(debounceTimeout);
    //         }
    //         debounceTimeout = setTimeout(setDimensions, 200);
    //     }
    //     window.addEventListener('resize', debouncedSetDimensions, false);
    //     setDimensions();
    // }

    // window.UnityLoader.Error.handler = function () { }

    // const i = 0;
    // new MutationObserver(function (mutationsList) {
    //     const canvas = getCanvasFromMutationsList(mutationsList)
    //     if (canvas){
    //         gCanvasElement = canvas;
    //         registerCanvasWatcher();

    //         new MutationObserver(function (attributesMutation) {
    //             this.disconnect();
    //             setTimeout(setDimensions, 1)
    //             q('.simmer').classList.add('hide');
    //         }).observe(canvas, {attributes:true});

    //         this.disconnect();
    //     }
    // }).observe(gameContainer, {childList:true});

})();