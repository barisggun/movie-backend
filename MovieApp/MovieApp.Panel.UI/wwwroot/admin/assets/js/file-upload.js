(function($) {
  'use strict';
  $(function() {
    $('.file-upload-browse').on('click', function() {
      var file = $(this).parent().parent().parent().find('.file-upload-default');
      file.trigger('click');
    });
      $('.file-upload-default').on('change', function () {
          var filename = $(this).val().replace(/C:\\fakepath\\/i, '');
          $(this).parent().find('.form-control').val(filename);
      });
  });
})(jQuery);