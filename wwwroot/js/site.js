// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// function rain(){
//     let amount = 15;
//     let body = document.querySelector('body');
//     let i = 0;
//     while (i < amount) {
//         let drop = document.createElement('i');

//         let size = Math.random() * 5;
//         let posX = Math.floor(Math.random() * window.innerWidth);

//         let deley = Math.random() * -20;
//         let duration = Math.random() * 5;


//         drop.style.width = 0.2 + size + 'px';
//         drop.style.left = posX + 'px';
//         drop.style.animationDelay = deley + 's';
//         drop.style.animationDuration = 1 + duration + 's';
//         body.appendChild(drop);
//         i++
//     }
//   }

//   rain();


const coords = { x: 0, y: 0};
const circles = document.querySelectorAll(".circle");

circles.forEach(function (circle) {
    circle.x = 0;
    circle.y = 0;
});

window.addEventListener('mousemove', function (e){

    coords.x = e.clientX;
    coords.y = e.clientY;

});

function animateCircles() {
    let x = coords.x;
    let y = coords.y;

    circles.forEach(function (circle, index) {
        circle.style.left = x - 7  + "px";
        circle.style.top = y - 7 + "px";

        circle.style.scale = (circles.length - index) / circles.length;

        circle.x = x;
        circle.y = y;

        const nextCircle = circles[index + 1] || circles[0];
        x += (nextCircle.x - x) * 0.3;
        y += (nextCircle.y - y) * 0.3;


        
    });
      
    requestAnimationFrame(animateCircles);
}// console.log("datoo")


animateCircles();

