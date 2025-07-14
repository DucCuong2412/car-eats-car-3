using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
	private bool isEscape;

	public void Init()
	{
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape))
		{
			return;
		}
		switch (Game.currentState)
		{
		case Game.gameState.Comics:
		case Game.gameState.ToDoList:
		case Game.gameState.FinishLose:
		case Game.gameState.PrevState:
		case Game.gameState.Tutorial:
			break;
		case Game.gameState.Shop:
			break;
		case Game.gameState.OpenWindow:
			break;
		case Game.gameState.Menu:
			if (!Game.PopInnerState())
			{
				LevelGalleryCanvasView levelGalleryCanvasView2 = UnityEngine.Object.FindObjectOfType<LevelGalleryCanvasView>();
				levelGalleryCanvasView2.LeaveGame.SetActive(!levelGalleryCanvasView2.LeaveGame.activeSelf);
			}
			break;
		case Game.gameState.Levels:
			if (!Game.PopInnerState())
			{
				LevelGalleryCanvasView levelGalleryCanvasView = UnityEngine.Object.FindObjectOfType<LevelGalleryCanvasView>();
				levelGalleryCanvasView.LeaveGame.SetActive(!levelGalleryCanvasView.LeaveGame.activeSelf);
			}
			break;
		case Game.gameState.PreRace:
		{
			CloseVideo closeVideo = UnityEngine.Object.FindObjectOfType<CloseVideo>();
			if (closeVideo != null)
			{
				closeVideo.Close();
				break;
			}
			FortuneNEw fortuneNEw = UnityEngine.Object.FindObjectOfType<FortuneNEw>();
			if (fortuneNEw != null)
			{
				fortuneNEw.TryExit();
			}
			break;
		}
		case Game.gameState.Race:
			if (!Game.PopInnerState() && Time.timeScale != 0f)
			{
				RaceLogic.instance.PauseRace();
			}
			break;
		case Game.gameState.PausedRace:
			if (!Game.PopInnerState())
			{
				RaceLogic.instance.ResumeRace();
			}
			break;
		case Game.gameState.Finish:
			if (!Game.PopInnerState())
			{
				if (Progress.levels.InUndeground)
				{
					Game.LoadLevel("scene_underground_map_new");
				}
				else
				{
					Game.LoadLevel("map_new");
				}
			}
			break;
		case Game.gameState.Monstropedia:
			if (!Game.PopInnerState())
			{
				if (Progress.levels.InUndeground)
				{
					Game.LoadLevel("scene_underground_map_new");
				}
				else
				{
					Game.LoadLevel("map_new");
				}
			}
			break;
		case Game.gameState.Fortune:
			Game.PopInnerState();
			break;
		case Game.gameState.Revive:
			RaceLogic.instance.GameOver();
			break;
		}
	}

	private void Escape()
	{
		if (isEscape)
		{
			Application.Quit();
		}
		else
		{
			StartCoroutine(escapeTrueForSeconds(1.2f));
		}
	}

	private IEnumerator escapeTrueForSeconds(float delay)
	{
		isEscape = true;
		yield return new WaitForSeconds(delay);
		isEscape = false;
	}
}
