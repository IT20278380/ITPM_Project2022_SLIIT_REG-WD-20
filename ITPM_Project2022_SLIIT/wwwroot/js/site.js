.   function validatePhoneNumber(input_str) {
       var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
       return re.test(input_str);
   }

    function validateemail(input_str) {
        var re = /[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{1,63}$/;
        return re.test(input_str);
    }

   function validateForm(event) {
       var phone = document.getElementById('mobileNumber').value;
       var email = document.getElementById('email').value;

       //Mobile Number
       if (!validatePhoneNumber(phone)) {
           document.getElementById('phone_error').classList.remove('hidden');
           event.preventDefault();
       } else {
           document.getElementById('phone_error').classList.add('hidden');
       }

      //Email
       if (!validateemail(email)) {
           document.getElementById('email_error').classList.remove('hidden');
           event.preventDefault();
       } else {
           document.getElementById('email_error').classList.add('hidden');
       }

   }