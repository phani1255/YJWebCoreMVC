
/** 
 * venkat 12/31/2025  added 
 */
(function () {

    let isChanged = false;

    // 🔹 Initialize default state (REFRESH *)
    $(function () {
        $('[data-refresh-default="changed"]').each(function () {
            isChanged = true;
            $(this)
                .addClass('is-changed')
                .html('<i class="fa fa-sync me-1"></i> REFRESH *');
        });
    });

    // 🔹 Any change inside refresh scope → mark changed
    $(document).on(
        'change',
        '[data-refresh-scope] input, [data-refresh-scope] select',
        function () {
            isChanged = true;
            $('[data-refresh]')
                .addClass('is-changed')
                .removeClass('is-working')
                .prop('disabled', false)
                .html('<i class="fa fa-sync me-1"></i> REFRESH *');
        }
    );

    // 🔹 Refresh button click
    $(document).on('click', '[data-refresh]', function () {

        if (!isChanged) return;

        let btn = $(this);

        btn
            .removeClass('is-changed')
            .addClass('is-working')
            .prop('disabled', true)
            .html('<i class="fa fa-spinner fa-spin me-1"></i> Working');

        setTimeout(function () {
            isChanged = false;
            btn
                .removeClass('is-working')
                .prop('disabled', false)
                .html('<i class="fa fa-sync me-1"></i> Refresh');
        }, 1500);
    });

})();
