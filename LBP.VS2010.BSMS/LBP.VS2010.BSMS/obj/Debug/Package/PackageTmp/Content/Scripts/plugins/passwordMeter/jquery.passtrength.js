;(function($, window, document, undefined) {

  var pluginName = "passtrength",
      defaults = {
        minChars: 8,
        passwordToggle: false,
        tooltip: true,
        textWeak: "Weak",
        textMedium: "Medium",
        textStrong: "Strong",
        textVeryStrong: "Very Strong",
        eyeImg : "img/eye.svg"
      };

  function Plugin(element, options){
    this.element = element;
    this.$elem = $(this.element);
    this.options = $.extend({}, defaults, options);
    this._defaults = defaults;
    this._name = pluginName;
    _this      = this;
    this.init();
  }

  Plugin.prototype = {
    init: function(){
      var _this    = this,
          meter    = jQuery("<div/>", {class: "passtrengthMeter"}),
          tooltip = jQuery("<div/>", {class: "tooltip", text: "Min " + this.options.minChars + " chars"});

      meter.insertAfter(this.element);
      $(this.element).appendTo(meter);

      if(this.options.tooltip){
        tooltip.appendTo(meter);
        var tooltipWidth = tooltip.outerWidth() / 2;
        tooltip.css("margin-left", -tooltipWidth);
      }

      

      //this.$elem.bind("keyup keydown", function() {
      this.$elem.bind("keyup", function() {
          value = $(this).val();
          _this.check(value);
      });

      if(this.options.passwordToggle){
        _this.togglePassword();
      }

    },

    check: function(value){
      var secureTotal  = 0,
          chars        = 0,
          capitals     = 0,
          numbers      = 0,
          special      = 0,
          wordRep      = 0;
          upperCase    = new RegExp("[A-Z]"),
          numbers      = new RegExp("[0-9]"),
          specialchars = new RegExp("([!,%,&,@,#,$,^,*,?,_,~])"),
          wordRepetitions = new RegExp("(.)\\1\\1");

       
      var errMsg = "",
          _LINE_BREAK = "&#10;";


      if(value.length >= this.options.minChars){
        chars = 1;
      }else{
        chars = -4;
      }

      if(value.match(upperCase)){
        capitals = 1;
      }else{
        capitals = 0;
        errMsg += _LINE_BREAK + "Password should contain uppercase letters."; 
      }
      if(value.match(numbers)){
        numbers = 1;
      }else{
        numbers = 0;
        errMsg += _LINE_BREAK + "Password should contain numbers/numerals."; 
      }
      if(value.match(specialchars)){
        special = 1;
      }else{
        special = 0;
        errMsg += _LINE_BREAK + "Password should contain special characters [!,%,&,@,#,$,^,*,?,_,~]."; 
      }
      

      if (!value.match(wordRepetitions)) {
        wordRep = 1;           
      }
      else {
        wordRep = 0;
        errMsg += _LINE_BREAK + "Password has too many repetitions";      
      }

      secureTotal = chars + capitals + numbers + special + wordRep;
      securePercentage = (secureTotal / 5) * 100;

      console.log(securePercentage);
     
        
      this.addStatus(securePercentage, securePercentage >= 20 ? errMsg: "");

    },

    addStatus: function(percentage, errMsg){
      var status = "",
          text = "Min " + this.options.minChars + " chars",
          meter = $(this.element).closest(".passtrengthMeter"),
          tooltip = meter.find(".tooltip");


      meter.attr("class", "passtrengthMeter");      

      if(percentage >= 20){
        meter.attr("class", "passtrengthMeter");
        status = "weak";
        text = this.options.textWeak;
      }
      if(percentage >= 60){
        meter.attr("class", "passtrengthMeter");
        status = "medium";
        text = this.options.textMedium;
      }
      if(percentage >= 80){
        meter.attr("class", "passtrengthMeter");
        status = "strong";
        text = this.options.textStrong;
      }
      if(percentage >= 100){
        meter.attr("class", "passtrengthMeter");
        status = "very-strong";
        text = this.options.textVeryStrong;
      }
      meter.addClass(status);
      if(this.options.tooltip){
      
       // tooltip.text(text + errMsg);        
        //tooltip.html(text + errMsg);        
        $(tooltip).html(text + errMsg);
        $(tooltip).css({"left": percentage >= 20 && percentage < 100 ? "20%": "50%"});
      }
    },

    togglePassword: function(){
    
//      var buttonShow = jQuery("<span/>", {class: "showPassword", html: "<img src="+ this.options.eyeImg +" />"}),
//          input      =  jQuery("<input/>", {type: "text"}),
//          passwordInput      = this;
  var buttonShow = '<span class="showPassword" id="spanShowPassword' + $(_this.element).attr('id') + '" data-parent-control="'+ $(_this.element).attr('id') + '" ><i class="fa fa-eye"></i></span>',
        buttonNoShow = '<span class="showPassword" style="display:none;"id="spanNoShowPassword'+ $(_this.element).attr('id') + '" data-parent-control="'+ $(_this.element).attr('id') + '" ><i class="fa fa-eye-slash"></i></span>',
          input      =  jQuery("<input/>", {class: "form-control", type: "text", id: "txtPasswordShow" + $(_this.element).attr('id') }),
          passwordInput      = this;

      $(buttonShow).appendTo($(this.element).closest(".passtrengthMeter"));
      $(buttonNoShow).appendTo($(this.element).closest(".passtrengthMeter"));
      input.appendTo($(this.element).closest(".passtrengthMeter")).hide();

      $(this.element).bind("keyup keydown", function() {
          input.val($(passwordInput.element).val());
      });

      input.bind("keyup keydown", function() {
          $(passwordInput.element).val(input.val());
          value = $(this).val();
          _this.check(value);
      });

//      $(document).on("click", ".showPassword" , function() {     
//        alert($(this).data('parent-control'));
//        $(buttonShow).toggleClass("active");
//        input.toggle();
//        $(passwordInput.element).toggle();
//      });
    }
  };

  $.fn[pluginName] = function(options) {
      return this.each(function() {
          if (!$.data(this, "plugin_" + pluginName)) {
              $.data(this, "plugin_" + pluginName, new Plugin(this, options));
          }
      });
  };

})(jQuery, window, document);


