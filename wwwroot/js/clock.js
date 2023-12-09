countDownTime = localStorage.getItem('countDownTime');

// If it's not in localStorage, initialize it to 24 hours
if (countDownTime === null) {
    countDownTime = 24 * 60 * 60; //24
} else {
    // Convert the stored value to a number
    countDownTime = parseInt(countDownTime);
}

function updateCountDown() {
    let hours = Math.floor(countDownTime / 3600);
    let minutes = Math.floor((countDownTime % 3600) / 60);   //here was constants
    let seconds = countDownTime % 60;

    const countDownElement = document.getElementById('time');

    if (hours < 10) {
        hours = '0' + hours.toString();
    }
    if (minutes < 10) {
        minutes = '0' + minutes.toString();
    }
    if (seconds < 10) {
        seconds = '0' + seconds.toString();
    }
    countDownElement.textContent = `${hours} : ${minutes} : ${seconds}`;

    console.log(`${hours} : ${minutes} : ${seconds}`);   //this is for debuging

    // countDownTime--;


    localStorage.setItem('countDownTime', countDownTime.toString());

    if (countDownTime < 22*60 * 60) {   //0
        SiecleDone(countDownElement);
        countDownTime = 24 * 60 * 60;  //24

        localStorage.setItem('countDownTime', countDownTime.toString());
    }
}
setInterval(updateCountDown, 500);

function SiecleDone(element) {
    let chars = ['&', '<', ')', '8', '^', '!', "%", '#', '}', '+', '_', '=', '-', '='];

    for (let i = 1; i <= 50; i++) {
        let one = chars[getRandomInt(0, chars.length)] + chars[getRandomInt(0, chars.length)];
        let two = chars[getRandomInt(0, chars.length)] + chars[getRandomInt(0, chars.length)];
        let three = chars[getRandomInt(0, chars.length)] + chars[getRandomInt(0, chars.length)];

        element.textContent = one + ' : ' + two + ' : ' + three;
    }
}
function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
}
async function delayedAction() {
    console.log('Before delay');
    await new Promise((resolve) => setTimeout(resolve, 1000));
    console.log('After 2 seconds');
}