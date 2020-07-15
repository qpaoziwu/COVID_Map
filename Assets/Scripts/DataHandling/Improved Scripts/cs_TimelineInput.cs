using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Alternate controls to the timeline complete with a play, play backward, skip forward, skip backward, max and min, and a loop button.
/// </summary>
public class cs_TimelineInput : MonoBehaviour
{
    [Header("Options")]

    [Tooltip("Set the seconds between play intervals")]
    public float m_seconds = 1;

    [Header("Setup Inputs")]

    [Tooltip("Insert the Timeline")]
    public Slider m_timeline;

    [Tooltip("Insert Play toggle")]
    public Toggle m_play;

    [Tooltip("Insert Play Backward toggle")]
    public Toggle m_playBack;

    [Tooltip("insert Loop Toggle")]
    public Toggle m_loop;

    private IEnumerator Playing;
    private IEnumerator PlayingBackward;

    #region Play
    /// <summary>
    /// Restarts at end of timeline if currently at the beginning
    /// Triggers Play Forward
    /// </summary>
    public void Play()
    {
        if (m_play.isOn == true)
        {
            m_playBack.isOn = false;

            if (m_timeline.value <= m_timeline.minValue)    // restarts the timeline if played at end
            {
                m_timeline.value = m_timeline.maxValue;
                Playing = Player();
                StartCoroutine(Playing);
            }
            else   // continues playing forward along timeline
            {
                Playing = Player();
                StartCoroutine(Playing);
            }
        }
        else
        {
            StopCoroutine(Playing);
        }
    }

    /// <summary>
    /// Plays forward through timeline and loops if active
    /// </summary>
    /// <returns></returns>
    public IEnumerator Player()
    {
        yield return new WaitForSeconds(m_seconds);

        for (int i = 0; i < m_timeline.maxValue; i++)
        {
            m_timeline.value--;

            yield return new WaitForSeconds(m_seconds);

            if (m_timeline.value <= m_timeline.minValue)
            {
                if (m_loop.isOn == true)
                {
                    m_timeline.value = m_timeline.maxValue;

                    Playing = Player();
                    StartCoroutine(Playing);
                }

                else { m_play.isOn = false; }
            }
        }
    }
    #endregion

    #region Play Backward
    /// <summary>
    /// Restarts at beginning of timeline if currently at the end
    /// Triggers Play Backward
    /// </summary>
    public void PlayBack()
    {
        if (m_playBack.isOn == true)
        {
            m_play.isOn = false;
            if (m_timeline.value >= m_timeline.maxValue)    //restarts the timeline if played at beginning
            {
                m_timeline.value = m_timeline.minValue;
                PlayingBackward = PlayBacker();
                StartCoroutine(PlayingBackward);
            }
            else   // continues playing backward along timeline
            {
                PlayingBackward = PlayBacker();
                StartCoroutine(PlayingBackward);
            }
        }
        else
        {
            StopCoroutine(PlayingBackward);
        }
    }

    /// <summary>
    /// Plays backward through timeline and loops if active
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayBacker()
    {
        yield return new WaitForSeconds(m_seconds);

        for (int i = 0; i < m_timeline.maxValue; i++)
        {
            m_timeline.value++;

            yield return new WaitForSeconds(m_seconds);

            if (m_timeline.value >= m_timeline.maxValue)
            {
                if (m_loop.isOn == true)
                {
                    m_timeline.value = m_timeline.minValue;

                    PlayingBackward = PlayBacker();
                    StartCoroutine(PlayingBackward);
                }

                else { m_playBack.isOn = false; }
            }
        }
    }
    #endregion

    #region Skip Forward
    /// <summary>
    /// skips a single value forward on the timeline
    /// </summary>
    public void SkipForward()
    {
        if (m_timeline.value <= m_timeline.minValue)    // moves to beginning of timeline if at max value
        {
            m_timeline.value = m_timeline.maxValue;
        }
        else { m_timeline.value--; }

        if (m_play.isOn == true)
        {
            StopCoroutine(Playing);
            m_play.isOn = false;
        }
        else if (m_playBack.isOn == true)
        {
            StopCoroutine(PlayingBackward);
            m_playBack.isOn = false;
        }
    }
    #endregion

    #region Skip Backward
    /// <summary>
    /// skips a single value backward on the timeline
    /// </summary>
    public void SkipBackward()
    {
        if (m_timeline.value >= m_timeline.maxValue)    // moves to end of timeline if at min value
        {
            m_timeline.value = m_timeline.minValue;
        }
        else { m_timeline.value++; }

        if (m_play.isOn == true)
        {
            StopCoroutine(Playing);
            m_play.isOn = false;
        }
        else if (m_playBack.isOn == true)
        {
            StopCoroutine(PlayingBackward);
            m_playBack.isOn = false;
        }
    }
    #endregion

    #region Max Forward
    /// <summary>
    /// skips to end of timeline
    /// </summary>
    public void MaxForward()
    {
        m_timeline.value = m_timeline.minValue;

        if (m_play.isOn == true)    // restarts the coroutine from new value
        {
            StopCoroutine(Playing);
            Playing = Player();
            StartCoroutine(Playing);
        }
        else if (m_playBack.isOn == true)    // restarts the coroutine from new value
        {
            StopCoroutine(PlayingBackward);
            PlayingBackward = PlayBacker();
            StartCoroutine(PlayingBackward);
        }
    }
    #endregion

    #region Max Backward
    /// <summary>
    /// skips to beginning of timeline
    /// </summary>
    public void MaxBackward()
    {
        m_timeline.value = m_timeline.maxValue;

        if (m_play.isOn == true)    // restarts the coroutine from new value
        {
            StopCoroutine(Playing);
            Playing = Player();
            StartCoroutine(Playing);
        }
        else if (m_playBack.isOn == true)    // restarts the coroutine from new value
        {
            StopCoroutine(PlayingBackward);
            PlayingBackward = PlayBacker();
            StartCoroutine(PlayingBackward);
        }
    }
    #endregion
}