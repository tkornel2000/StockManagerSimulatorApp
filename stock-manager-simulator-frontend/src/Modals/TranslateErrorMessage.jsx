function translateErrorMessage(errorMessage) {
    switch (errorMessage) {
        case 'NAME_IS_REQUIRED':
            return 'A név megadása kötelező.';
        case 'EMAIL_IS_REQUIRED':
            return 'Az email megadása kötelező.';
        case 'INVALID_EMAIL_FORMAT':
            return 'Érvénytelen email formátum.';
        case 'THERE_IS_USER_WITH_THIS_EMAIL':
            return 'Már van felhasználó ezzel az email címmel.';
        case 'PASSWORD_IS_REQUIRED':
            return 'A jelszó megadása kötelező.';
        case 'PASSWORD_IS_TOO_SHORT':
            return 'A jelszó túl rövid.';
        case 'PASSWORD_MUST_HAVE_UPPERCASE_LETTER':
            return 'A jelszónak tartalmaznia kell legalább egy nagybetűt.';
        case 'PASSWORD_MUST_HAVE_LOWERCASE_LETTER':
            return 'A jelszónak tartalmaznia kell legalább egy kisbetűt.';
        case 'PASSWORD_MUST_HAVE_NUMBER':
            return 'A jelszónak tartalmaznia kell legalább egy számot.';
        case 'THERE_IS_USER_WITH_THIS_USERNAME':
            return 'Már van felhasználó ezzel a felhasználónévvel.';
        case 'CONFIRMPASSWORD_IS_REQUIRED':
            return 'A jelszó megerősítése kötelező.';
        case 'CONFIRMPASSWORD_IS_NOT_MATCHED_WITH_PASSWORD':
            return 'A jelszó megerősítése nem egyezik a jelszóval.';
        case 'THERE_IS_NO_USER_WITH_THIS_ID':
            return 'Nincs felhasználó ezzel az azonosítóval.';
        case 'YOU_CAN_NOT_DELETE_THIS_ACCOUNT_BECAUSE_PASSWORD_IS_INVALID':
            return 'Nem törölheted ezt a fiókot, mert érvénytelen jelszó.';
        case 'YOUR_OLD_PASSWORD_WAS_WRONG':
            return 'Régi jelszavad helytelen volt.';
        case 'THERE_IS_NO_USER_WITH_THIS_USERNAME':
            return 'Nincs felhasználó ezzel a felhasználónévvel.';
        case 'WRONG_PASSWORD':
            return 'Hibás jelszó.';
        case 'THERE_IS_AN_UNEXPECTED_ERROR':
            return 'Váratlan hiba történt.';
        case 'THERE_IS_NO_USER_WITH_THIS_EMAIL':
            return 'Nincs felhasználó ezzel az email címmel.';
        case 'BIRTH_OF_DATE_IS_REQUIRED':
            return 'A születési dátum megadása kötelező.';
        case 'GENDER_IS_REQUIRED':
            return 'A nem megadása kötelező.';
        case 'YOU_DO_NOT_HAVE_ENOUGH_MONEY':
            return 'Nincs elég pénzed.';
        case 'YOU_CAN_NOT_BUY_ZERO_OR_MINUS_NUMBER_OF_STOCK':
            return 'Nem vásárolhatsz nullát vagy negatív számú részvényt.';
        case 'YOU_CAN_NOT_SELL_THAT_MANY_STOCK_BECAUSE_YOU_DO_NOT_HAVE_THAT_MANY':
            return 'Nem adhatsz el ennyi részvényt, mert nincs ennyi a birtokodban.';
        case 'YOU_CAN_NOT_SELL_ZERO_OR_MINUS_NUMBER_OF_STOCK':
            return 'Nem adhatsz el nullát vagy negatív számú részvényt.';
        case 'THERE_IS_NO_STOCK_WITH_THIS_SYMBOL':
            return 'Nincs részvény ezzel a szimbólummal.';
        case 'INVALID_BIRTH_OF_DATE':
            return 'Érvénytelen születési dátum formátum.';
        case 'SERVER_ERROR':
            return 'A szerver jelenleg nem elérhető';
        default:
            return 'Ismeretlen hiba történt.';
    }
}

export default translateErrorMessage;
