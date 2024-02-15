
        /*   here is js stuff of that tutorial above and it probably needs fix at list in url

        function submitForm() {
            var name = document.getElementById("name").value;
            var email = document.getElementById("email").value;

            //  Send data to the backend
            $.ajax({
                type: "POST",
                url: "/Home/ProcessData",
                data: { name: name, email: email },
                success: function(response) {
                    // Handle the response from the backend
                    console.log(response);
                } ,
                error: function(error) {
                    console.error('Error:', error);
                }
            });
        }

        */

        // all of js code that was here is now in clock.js file in wwwRout folder



        
//terminal code here 
// const consoleOutput = document.getElementById('output');
// const consoleInput = document.getElementById('input');

// consoleInput.addEventListener('keydown', function (event) {
//     if (event.key === 'Enter') {
//         const command = consoleInput.value;
//         consoleInput.value = ''; // Clear input field
//         processCommand(command);
//     }
// });

function processCommand(command) {
    // Implement your command processing logic here
    // Display the result in the consoleOutput div

    consoleOutput.innerHTML += `<p>${command}</p>`;

    if (command === '*help' || command === '*h' || command === '*?') {
        consoleOutput.innerHTML += `<p>Available commands: [none RN]</p>`;
    }
    if (command === '*clear' || command === '*c' || command === '*"') {
        consoleOutput.innerHTML = '';
    }
    // more comands go here  else if ()
    else {
        consoleOutput.innerHTML += `<p>Command not recognized: ${command}</p>`;
    }
}

// async function fetchData() {
//     try {
//         const response = await fetch('api');
//         if (!response.ok) {
//             throw new Error(`HTTP error: ${response.status}`);
//         }
//         const data = await response.text();
//         console.log(data);
//     } catch (error) {
//         console.error('There was a problem with the fetch operation:', error);
//     }
// }
// fetchData();

