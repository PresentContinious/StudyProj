import styles from './test-component.module.css';
import {useState} from "react";
import {apiEndpoint} from "../../api";
import {useNavigate} from "react-router-dom";
import {launchError} from "../layout/Layout";

const TestComponent = ({hidden, questions, courseId, show, testData, mark}) => {
    const navigate = useNavigate();

    const questionsPerPage = hidden ? 3 : 2;
    const maxPage = Math.ceil(questions.length / questionsPerPage);

    const [page, setPage] = useState(1);
    const [data, setData] = useState([]);

    const indexOfLastQuestion = page * (hidden ? 3 : 2);
    const indexOfFirstQuestion = indexOfLastQuestion - questionsPerPage;
    const currentQuestions = questions.slice(indexOfFirstQuestion, indexOfLastQuestion);

    const reducePage = () => {
        if (page > 1) {
            setPage(page - 1);
        }
    }

    const increasePage = () => {
        if (page < maxPage) {
            setPage(page + 1);
        }
    }

    const addData = (questionId, answerId) => {
        let newData = data.filter((item) => item.questionId !== questionId);
        newData.push({questionId, answerId});
        setData(newData);
    }

    const handleClick = () => {
        const savedData = data;
        setData([]);

        apiEndpoint('course/next/' + courseId).post({quizAnswers: savedData})
            .then(res => {
                if (res.data.navigate)
                    navigate(res.data.navigate);
                else
                    navigate(0);
            }).catch(err => {
            launchError(err);
            setData(savedData);
        });
    }

    return (
        <div className={styles.group}>
            <div className={styles.markWrap}>
                <p className={styles.p}>Тест 3: Lorem ipsum dolor sim loperic</p>
                {mark && <div className={styles.mark}>Відсоток правильних відповідей: {mark}%</div>}
            </div>
            <div className={styles.questions} style={{gap: `${!hidden ? '30px' : ''}`}}>
                {
                    currentQuestions.map((question, index) =>
                        <QuestionAndAnswersWrapper key={'Question' + question.id} question={question}
                                                   addData={addData}
                                                   test={!show ? testData.filter(q => q.questionId === question.id)[0] : null}
                                                   index={indexOfFirstQuestion + index + 1}/>
                    )
                }
            </div>
            <div className={styles.div5} style={{marginTop: `${hidden ? '30px' : ''}`}}>
                <div className={styles.pagination}>
                    <div className={styles.overlapGroup}>
                        <div className={styles.rectangle1} onClick={reducePage}>
                            <img
                                className={`${styles.icRoundNavigate}`}
                                alt="Ic round navigate"
                                src={"https://c.animaapp.com/UJbFoveO/img/ic-round-navigate-next-10.svg"}
                            />
                        </div>
                    </div>
                    <Pagination totalPages={maxPage} activePage={page} setPage={setPage}/>
                    <div className={styles.overlapGroup}>
                        <div className={styles.rectangle} onClick={increasePage}>
                            <img
                                className={`${styles.img}`}
                                alt="Ic round navigate"
                                src={"https://c.animaapp.com/UJbFoveO/img/ic-round-navigate-next-11.svg"}
                            />
                        </div>
                    </div>
                </div>
                {show && <div className={`${styles.propertyFrameWrapper}
                 ${data.length === questions.length ? '' : styles.disabled}`}
                              onClick={handleClick}>
                    <div className={styles.textWrapper2}>Зберегти відповіді</div>
                </div>}
            </div>
        </div>
    )
}

const QuestionAndAnswersWrapper = ({index, question, addData, test}) => {
    const [checked, setChecked] = useState(test ? test.answerId : -1);

    const handleAnswer = (answerId) => {
        if (test)
            return;

        setChecked(answerId);
        addData(question.id, answerId);
    }

    return (
        <div>
            <div className={styles.title}>
                <p className={styles.div2}>{index}.</p>
                <p className={styles.div2}>{question.text}</p>
            </div>
            <div className={styles.answers}>
                {
                    question['answers']?.map((answer) =>
                        <div className={`${styles.divWrapper}`} key={'Answer' + answer.id}
                             onClick={() => handleAnswer(answer.id)}>
                            <div className={styles.ellipse}>
                                {checked === answer.id && <div className={styles.div}/>}
                            </div>
                            <div className={styles.textWrapper}>{answer.text}</div>
                        </div>
                    )}
            </div>
        </div>
    );
};

const Pagination = ({totalPages, activePage, setPage}) => {
    const pages = [];

    for (let i = 1; i <= totalPages; i++) {
        if (i === 1 || i === totalPages || i === activePage || Math.abs(activePage - i) <= 2) {
            pages.push(i);
        }
    }

    const setPageHandler = (page) => {
        if (page !== activePage) {
            setPage(page);
        }
    }

    return (
        <div className={styles.rectangle2}>
            <div className={styles.overlap}>
                {pages[0] > 1 && (
                    <>
                        <div className={styles.element2} onClick={() => setPageHandler(1)}>1</div>
                        {pages[0] > 2 && <div className={styles.textWrapper3}>...</div>}
                    </>
                )}

                {pages.map((page) => {
                    if ((page - activePage === 2 && totalPages - page > 1) || (activePage - page === 2 && page > 1))
                        return <div className={styles.textWrapper3}>...</div>

                    return <div className={page === activePage ? styles.element1 : styles.element2}
                                key={'page' + page} onClick={() => setPageHandler(page)}>{page}</div>
                })}

                {pages[pages.length - 1] < totalPages && (
                    <>
                        {pages[pages.length - 1] < totalPages - 1 && <div className={styles.textWrapper3}>...</div>}
                        <div className={styles.element2} onClick={() => setPageHandler(totalPages)}>{totalPages}</div>
                    </>
                )}
            </div>
        </div>
    );
}

export default TestComponent;
