$(document).ready(function () {
    $('.scan-accordion').each(function () {
        var $accordion = $(this);
        var entityId = $accordion.data('entity-id');
        var entityType = $accordion.data('entity-type');
        var $thumbnails = $accordion.find('.thumbnails');
        var $noThumbnails = $accordion.find('.no-thumbnails');
        var $leftButton = $accordion.find('.left-button');
        var $rightButton = $accordion.find('.right-button');
        var $contextMenu = $('.context-menu');
        var thumbnails = [];
        var currentIndex = 0;

        // Load thumbnails
        function loadThumbnails() {
            $.get('/ScanAccordion/GetThumbnails', { entityId: entityId, entityType: entityType }, function (data) {
                thumbnails = data;
                $thumbnails.empty();
                if (thumbnails.length === 0) {
                    $noThumbnails.show();
                    $contextMenu.find('[data-action="view"], [data-action="remove"]').hide();
                } else {
                    $noThumbnails.hide();
                    thumbnails.forEach(function (thumb, index) {
                        var $thumb = $('<div class="thumbnail" data-index="' + index + '" data-image-id="' + thumb.imageId + '"></div>');
                        $thumb.append(thumb.isPdf ? '<img src="/images/pdf-icon.png" alt="PDF" />' : '<img src="' + thumb.fileUrl + '" alt="' + thumb.fileName + '" />');
                        $thumb.append('<span>' + thumb.fileName + '</span>');
                        $thumbnails.append($thumb);
                    });
                    selectThumbnail(0);
                    $contextMenu.find('[data-action="view"], [data-action="remove"]').show();
                }
                updateButtons();
            });
        }

        // Select a thumbnail
        function selectThumbnail(index) {
            if (index >= 0 && index < thumbnails.length) {
                $thumbnails.find('.thumbnail').removeClass('selected').css('background-color', '');
                $thumbnails.find('.thumbnail[data-index="' + index + '"]').addClass('selected').css('background-color', '#00008b'); // SelectedColor
                currentIndex = index;
                updateButtons();
                $accordion.trigger('thumbnailChanged', { oldIndex: currentIndex, newIndex: index, thumbnail: thumbnails[index] });
            }
        }

        // Update navigation buttons
        function updateButtons() {
            $leftButton.prop('disabled', currentIndex === 0);
            $rightButton.prop('disabled', currentIndex === thumbnails.length - 1);
            $leftButton.find('i').toggleClass('text-muted', currentIndex === 0);
            $rightButton.find('i').toggleClass('text-muted', currentIndex === thumbnails.length - 1);
        }

        // Navigation button clicks
        $leftButton.click(function () {
            if (currentIndex > 0) {
                selectThumbnail(currentIndex - 1);
            }
        });

        $rightButton.click(function () {
            if (currentIndex < thumbnails.length - 1) {
                selectThumbnail(currentIndex + 1);
            }
        });

        // Thumbnail click
        $thumbnails.on('click', '.thumbnail', function () {
            var index = parseInt($(this).data('index'));
            selectThumbnail(index);
        });

        // Context menu handling
        $accordion.on('contextmenu', '.thumbnail, .no-thumbnails', function (e) {
            e.preventDefault();
            var $target = $(this);
            var index = $target.hasClass('thumbnail') ? parseInt($target.data('index')) : -1;
            $contextMenu.data('target-index', index).css({
                display: 'block',
                left: e.pageX,
                top: e.pageY
            });
            return false;
        });

        // Hide context menu on click elsewhere
        $(document).click(function () {
            $contextMenu.hide();
        });

        // Context menu actions
        $contextMenu.find('li').click(function () {
            var action = $(this).data('action');
            var index = parseInt($contextMenu.data('target-index'));
            $contextMenu.hide();

            if (action === 'attach-image' || action === 'attach-pdf') {
                var accept = action === 'attach-pdf' ? 'application/pdf' : 'image/*';
                $('#fileInput').attr('accept', accept).click();
            } else if (action === 'view' && index >= 0) {
                var thumb = thumbnails[index];
                if (thumb.isPdf) {
                    window.open(thumb.fileUrl, '_blank');
                } else {
                    var $modal = $('<div class="modal fade"><div class="modal-dialog"><div class="modal-content"><img style="width: 100%;" src="' + thumb.fileUrl + '" /></div></div></div>');
                    $modal.modal('show').on('hidden.bs.modal', function () { $modal.remove(); });
                }
            } else if (action === 'remove' && index >= 0) {
                var thumb = thumbnails[index];
                $.post('/ScanAccordion/DeleteFile', { entityId: entityId, entityType: entityType, fileName: thumb.fileName }, function () {
                    loadThumbnails();
                });
            }
        });

        // File upload handling
        $('#fileInput').change(function () {
            var file = this.files[0];
            if (file) {
                var formData = new FormData();
                formData.append('file', file);
                formData.append('entityId', entityId);
                formData.append('entityType', entityType);
                $.ajax({
                    url: '/ScanAccordion/UploadFile',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (thumb) {
                        thumbnails.push(thumb);
                        loadThumbnails();
                    },
                    error: function () {
                        alert('Failed to upload file.');
                    }
                });
                this.value = ''; // Reset input
            }
        });

        // Initialize
        loadThumbnails();

        // Hover effect
        $thumbnails.on('mouseenter', '.thumbnail', function () {
            if (!$(this).hasClass('selected')) {
                $(this).css('background-color', '#800080'); // HoverColor
            }
        }).on('mouseleave', '.thumbnail', function () {
            if (!$(this).hasClass('selected')) {
                $(this).css('background-color', '');
            }
        });
    });
});