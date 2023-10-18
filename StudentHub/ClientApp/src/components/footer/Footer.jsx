import styles from './footer.module.css';

const Footer = () => {
    return (
        <div className={styles.footer}>
            <div className={styles.group}>
                <div className={styles.group2}>
                    <div className={styles.logo}>
                        <img alt="Img"
                             src="https://c.animaapp.com/BU0gcmOz/img/-----------------1.svg"/>
                        <div className={styles.textWrapper13}>© OnCourse, 2023</div>
                    </div>
                    <div className={styles.groupWrap}>
                        <div className={styles.textWrapper6}>Політика конфіденційності</div>
                        <div className={styles.textWrapper7}>Про нас</div>
                        <div className={styles.textWrapper8}>Політика для спільноти</div>
                        <div className={styles.textWrapper9}>Політика використання файлів cookie</div>
                    </div>
                    <div className={styles.groupWrap}>
                        <div className={styles.textWrapper6}>Служба підтримки</div>
                        <div className={styles.textWrapper7}>Мапа сайту</div>
                        <div className={styles.textWrapper8}>Договір публічної оферти</div>
                        <div className={styles.divWrapper}>
                            <div className={styles.textWrapper5}>Підписатися на новини</div>
                        </div>
                    </div>
                    <div className={styles.groupWrap}>
                        <div className={styles.textWrapper12}>Наші партнери:</div>
                        <div className={styles.frame3}>
                            <img alt="Logos mastercard"
                                 src="https://c.animaapp.com/BU0gcmOz/img/logos-mastercard-1.svg"/>
                            <img alt="Vector"
                                 src="https://c.animaapp.com/BU0gcmOz/img/vector-1.svg"/>
                        </div>
                    </div>
                    <div className={styles.groupWrap}>
                        <div className={styles.textWrapper12}>Контакти</div>
                        <p className={styles.p}>м. Київ, вул. Райдужна 25</p>
                        <p className={styles.textWrapper10}>+38 097 123 44 55</p>
                        <div className={styles.textWrapper11}>questions@oncourse.ua</div>
                        <div className={styles.icons}>
                            <img alt="Devicon twitter"
                                 src="https://c.animaapp.com/BU0gcmOz/img/devicon-twitter-1.svg"/>
                            <img alt="Logos facebook"
                                 src="https://c.animaapp.com/BU0gcmOz/img/logos-facebook-1.svg"/>
                            <img alt="Skill icons"
                                 src="https://c.animaapp.com/BU0gcmOz/img/skill-icons-instagram-1.svg"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Footer;
