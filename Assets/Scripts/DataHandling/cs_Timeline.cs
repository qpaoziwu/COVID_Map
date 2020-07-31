using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Additional controls to the timeline complete with a play, play backward, skip forward, skip backward, max and min, and a loop button.
/// </summary>
public class cs_Timeline : MonoBehaviour
{
    [Header("Options")]

    [Tooltip("Set the seconds between play intervals")]
    public float m_seconds = 1;

    [Header("Setup Inputs")]

    [Tooltip("Insert the GameObject that cs_CSVData is attached to")]
    public cs_CSVData m_CSV;

    [Tooltip("Insert the Timeline")]
    public Slider m_timelineSlider;

    [Tooltip("Insert Play toggle")]
    public Toggle m_play;

    [Tooltip("Insert Play Backward toggle")]
    public Toggle m_playBack;

    [Tooltip("insert Loop Toggle")]
    public Toggle m_loop;

    [Header("Visuals")]

    [Tooltip("Two points to spawn between, place copies of tick sprite at beginning and end of timeline")]
    public Transform[] m_tickPoints;

    [Tooltip("Tick to duplicate\nChange this to image or sprite or whatever you need.\nBe sure to also change in duplicateTick()")]
    public Image m_image;


    [HideInInspector]
    public int m_tickAmount; // the amount of tick points

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
            if (m_timelineSlider.value <= m_timelineSlider.minValue)    // restarts the timeline if played at end
            {
                m_timelineSlider.value = m_timelineSlider.maxValue;
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
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Plays forward through timeline and loops if active
    /// </summary>
    /// <returns></returns>
    public IEnumerator Player()
    {
        yield return new WaitForSeconds(m_seconds);

        for (int i = 0; i < m_timelineSlider.maxValue; i++)
        {
            m_timelineSlider.value--;

            yield return new WaitForSeconds(m_seconds);

            if (m_timelineSlider.value <= m_timelineSlider.minValue)
            {
                if (m_loop.isOn == true)
                {
                    StopAllCoroutines();
                    m_timelineSlider.value = m_timelineSlider.maxValue;

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
            if (m_timelineSlider.value >= m_timelineSlider.maxValue)    //restarts the timeline if played at beginning
            {
                m_timelineSlider.value = m_timelineSlider.minValue;
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
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Plays backward through timeline and loops if active
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayBacker()
    {
        yield return new WaitForSeconds(m_seconds);

        for (int i = 0; i < m_timelineSlider.maxValue; i++)
        {
            m_timelineSlider.value++;

            yield return new WaitForSeconds(m_seconds);

            if (m_timelineSlider.value >= m_timelineSlider.maxValue)
            {
                if (m_loop.isOn == true)
                {
                    m_timelineSlider.value = m_timelineSlider.minValue;

                    StopAllCoroutines();
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
        if (m_timelineSlider.value <= m_timelineSlider.minValue)    // moves to beginning of timeline if at max value
        {
            m_timelineSlider.value = m_timelineSlider.maxValue;
        }
        else { m_timelineSlider.value--; }

        if (m_play.isOn == true)
        {
            StopAllCoroutines(); ;
            m_play.isOn = false;
        }
        else if (m_playBack.isOn == true)
        {
            StopAllCoroutines();
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
        if (m_timelineSlider.value >= m_timelineSlider.maxValue)    // moves to end of timeline if at min value
        {
            m_timelineSlider.value = m_timelineSlider.minValue;
        }
        else { m_timelineSlider.value++; }

        if (m_play.isOn == true)
        {
            StopAllCoroutines();
            m_play.isOn = false;
        }
        else if (m_playBack.isOn == true)
        {
            StopAllCoroutines();
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
        m_timelineSlider.value = m_timelineSlider.minValue;

        if (m_play.isOn == true)    // restarts the coroutine from new value
        {
            StopAllCoroutines();
            Playing = Player();
            StartCoroutine(Playing);
        }
        else if (m_playBack.isOn == true)    // restarts the coroutine from new value
        {
            StopAllCoroutines();
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
        m_timelineSlider.value = m_timelineSlider.maxValue;

        if (m_play.isOn == true)    // restarts the coroutine from new value
        {
            StopAllCoroutines();
            Playing = Player();
            StartCoroutine(Playing);
        }
        else if (m_playBack.isOn == true)    // restarts the coroutine from new value
        {
            StopAllCoroutines();
            PlayingBackward = PlayBacker();
            StartCoroutine(PlayingBackward);
        }
    }
    #endregion

    /// <summary>
    /// Starts the DuplicateTick() method
    /// </summary>
    public void SpawnTicks()
    {
        DuplicateTick(m_image, m_tickAmount);
    }

    /// <summary>
    /// Duplicates ticks for the amount of dates in the CSV minus the two that are pre placed on the timeline
    /// </summary>
    private void DuplicateTick(Image original, int amount)      // If the tick file type is changed, set here
    {
        amount++;
        for (int i = 0; i < m_tickPoints.Length - 1; i++)
        {
            for (int x = 1; x < amount; x++)
            {
                Vector3 position = m_tickPoints[i].position + x * (m_tickPoints[i + 1].position - m_tickPoints[i].position) / amount;
                Instantiate(original, position, Quaternion.identity, transform.GetChild(0));
            }
        }
    }

    /// <summary>
    /// Stop all Coroutines
    /// </summary>
    public void DeadStop()
    {
        StopAllCoroutines();
    }
}