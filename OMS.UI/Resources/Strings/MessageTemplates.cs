namespace OMS.UI.Resources.Strings
{
    public static class MessageTemplates
    {
        // Not Implemented Message
        public static string NotImplementedMessage => "لم يتم انشاء هذه الاضافة";

        // Success Messages
        public static string SuccessMessage => "تم تنفيذ الاجراء بنجاح";
        public static string DeletionSuccessMessage => "تمت عملية الحذف بنجاح";
        public static string AdditionSuccessMessage => "تمت عملية الإضافة بنجاح";
        public static string UpdateSuccessMessage => "تمت عملية التعديل بنجاح";
        public static string SaveSuccessMessage => "تم حفظ البيانات بنجاح";
        public static string LoadSuccessMessage => "تم تحميل البيانات بنجاح";
        public static string VerificationSuccessMessage => "تم التحقق من البيانات بنجاح";

        // Error Messages
        public static string ErrorMessage => "حدث خطأ أثناء تنفيذ هذا الاجراء";
        public static string DeletionErrorMessage => "حدث خطأ أثناء عملية الحذف";
        public static string AdditionErrorMessage => "حدث خطأ أثناء عملية الإضافة";
        public static string UpdateErrorMessage => "حدث خطأ أثناء عملية التعديل";
        public static string SaveErrorMessage => "حدث خطأ أثناء عملية الحفظ";
        public static string LoadErrorMessage => "حدث خطأ أثناء عملية التحميل";
        public static string VerificationErrorMessage => "حدث خطأ أثناء عملية التحقق";
        public static string SearchErrorMessage => "حدث خطأ أثناء عملية البحث عن النموذج";

        // Confirmation Messages
        public static string DeletionConfirmation => "هل انت متأكد من إجراء هذه العملية؟\nسيتم خسارة جميع البيانات ولن يتم استرجاعها.";
        public static string AdditionConfirmation => "هل أنت متأكد من رغبتك في إضافة هذه البيانات؟";
        public static string UpdateConfirmation => "هل أنت متأكد من رغبتك في تعديل هذه البيانات؟";
        public static string SaveConfirmation => "هل أنت متأكد من رغبتك في حفظ هذه البيانات؟";
        public static string LoadConfirmation => "هل أنت متأكد من رغبتك في تحميل هذه البيانات؟";

        // Warning Messages
        public static string WarningMessage => "تحذير: هذا الإجراء قد يسبب فقدان البيانات.";

        // Validation Messages
        public static string FieldRequiredMessage => "هذا الحقل مطلوب.";
        public static string InvalidEmailFormatMessage => "صيغة البريد الإلكتروني غير صحيحة.";
        public static string FieldLengthExceededMessage => "تجاوز عدد الأحرف المسموح بها.";
        public static string InvalidNumberMessage => "الرجاء إدخال رقم صحيح.";
        public static string PasswordMismatchMessage => "كلمتا المرور غير متطابقتين.";
        public static string InvalidDateMessage => "التاريخ المدخل غير صالح.";
        public static string InvalidRangeMessage => "القيمة المدخلة خارج النطاق المسموح.";

        public static string ValidationErrorMessage(string? fieldName) => $"خطأ في عمليات الادخال:\n{fieldName}";

        // Login Success Messages
        public static string LoginSuccessMessage => "تم تسجيل الدخول بنجاح";
        public static string LogoutSuccessMessage => "تم تسجيل الخروج بنجاح";
        public static string RegistrationSuccessMessage => "تم إنشاء الحساب بنجاح";
        public static string PasswordResetSuccessMessage => "تم إعادة تعيين كلمة المرور بنجاح";
        public static string EmailConfirmationSuccessMessage => "تم تأكيد البريد الإلكتروني بنجاح";
        public static string AccountUnlockSuccessMessage => "تم فتح الحساب بنجاح";

        // Login Error Messages
        public static string LoginErrorMessage => "حدث خطأ أثناء تسجيل الدخول";
        public static string InvalidCredentialsErrorMessage => "بيانات الاعتماد غير صحيحة";
        public static string AccountLockedErrorMessage => "الحساب مقفل بسبب عدة محاولات فاشلة";
        public static string AccountInActiveErrorMessage => "الحساب الخص بك معطل";
        public static string AccountCantInActive => "لا يمكن الغاء تفعيل الحساب الذي تستخدمه حالياً";

        public static string LogoutErrorMessage => "حدث خطأ أثناء تسجيل الخروج";
        public static string RegistrationErrorMessage => "حدث خطأ أثناء إنشاء الحساب";
        public static string PasswordResetErrorMessage => "حدث خطأ أثناء إعادة تعيين كلمة المرور";
        public static string EmailConfirmationErrorMessage => "حدث خطأ أثناء تأكيد البريد الإلكتروني";
        public static string SessionExpiredErrorMessage => "انتهت صلاحية الجلسة، الرجاء تسجيل الدخول مجددًا";

        // Login Confirmation Messages
        public static string LogoutConfirmation => "هل أنت متأكد من رغبتك في تسجيل الخروج؟";
        public static string AccountDeletionConfirmation => "هل أنت متأكد من حذف الحساب بشكل دائم؟";


        public static string InvalidDeleteUserMessage => "لا يمكن حذف الحساب المستخدم لانك تستخدمه.";


        // Creating Client Account Messages

        public static string ClientAccountAdditionSuccess => "تم انشاء حساب الكتروني للعميل بنجاح";

        public static string ClientAccountAdditionError => "حدث خطأ اثناء عملية انشاء حساب الكتروني للعميل";

        public static string ClientAccountDeletionError => "حدث خطأ اثناء عملية حذف الحساب الكتروني الخاص في العميل";


        // Transaction Success Messages
        public static string DepositSuccessMessage => "تمت عملية الإيداع بنجاح";
        public static string WithdrawalSuccessMessage => "تمت عملية السحب بنجاح";
        public static string TransferSuccessMessage => "تمت عملية التحويل بنجاح";

        // Transaction Error Messages
        public static string DepositErrorMessage => "حدث خطأ أثناء عملية الإيداع";
        public static string WithdrawalErrorMessage => "حدث خطأ أثناء عملية السحب";
        public static string TransferErrorMessage => "حدث خطأ أثناء عملية التحويل";
        public static string InsufficientBalanceErrorMessage => "عملية السحب لم تتم بسبب عدم توفر رصيد كافٍ.\n يرجى التحقق من الرصيد والمحاولة مرة أخرى.";

        // Transaction Confirmation Messages
        public static string DepositConfirmation => "هل أنت متأكد من رغبتك في إجراء عملية الإيداع؟";
        public static string WithdrawalConfirmation => "هل أنت متأكد من رغبتك في إجراء عملية السحب؟";
        public static string TransferConfirmation => "هل أنت متأكد من رغبتك في إجراء عملية التحويل؟";

        // Transaction Validation Messages
        public static string InvalidTransactionAmountMessage => "المبلغ المدخل غير صالح. الرجاء إدخال قيمة موجبة";
        public static string InvalidRecipientAccountMessage => "رقم حساب المستلم غير صحيح. الرجاء التحقق والمحاولة مرة أخرى";

        // Cancel Messages

        public static string CancellationSaleConfirmation => "هل انت متأكد من إجراء هذه العملية؟\nسيتم الغاء المبيعة ولن تستطيع التعديل عليها.";
        public static string CancelSaleSuccessMessage => "تم عملية الغاء المبيعة بنجاح";
        public static string CancelSaleErrorMessage => "حدث خطأ اثناء عملية الغاء المبيعة";

        public static string CancellationDebtConfirmation => "هل انت متأكد من إجراء هذه العملية؟\nسيتم الغاء الدين ولن تستطيع التعديل عليه.";
        public static string CancelDebtSuccessMessage => "تم عملية الغاء الدين بنجاح";
        public static string CancelDebtErrorMessage => "حدث خطأ اثناء عملية الغاء الدين";


        // User Activation Confirmation

        public static string ActivateUserConfirmation => "هل انت متأكد من إجراء هذه العملية؟\nسيتم تفعيل الحساب الحالي.";
        public static string InActivateUserConfirmation => "هل انت متأكد من إجراء هذه العملية؟\nسيتم الغاء تفعيل الحساب الحالي.";

    }
}
