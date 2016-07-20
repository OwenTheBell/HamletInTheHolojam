using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

namespace Holojam{

	public class VRConsole : MonoBehaviour {

		string privateCache;
		public int numLinesDisplayed = 5;
		public bool linewrapOn = true;
		public int numCharsPerLine = 20;

		char[] newLineArray = new char[]{'\n'};

		// Use this for initialization
		void Start () {
			privateCache = "";
		}
		
		// Update is called once per frame
		public virtual void Update () {
			toggleDisplay ();
			//println (Time.time.ToString());
			//var centerEye = UnityEngine.VR.InputTracking.GetLocalPosition(UnityEngine.VR.VRNode.CenterEye);
			//var head = UnityEngine.VR.InputTracking.GetLocalPosition (UnityEngine.VR.VRNode.Head);
			//var leftEye = UnityEngine.VR.InputTracking.GetLocalPosition (UnityEngine.VR.VRNode.LeftEye);
			//var rightEye = UnityEngine.VR.InputTracking.GetLocalPosition (UnityEngine.VR.VRNode.RightEye);
			//VRConsoleDebug.println("CenterEye: (" + centerEye.x.ToString("F8") + "," + centerEye.y.ToString("F8") + "," + centerEye.z.ToString("F8") + ")");
			//print ("Head: " + head);
			//print ("LeftEye: " + leftEye);
			//print ("RightEye: " + rightEye);
			//VRConsoleDebug.println("CenterEye: " + Vector3.Distance(centerEye, Vector3.zero).ToString("F8"));
			//print ("Head: " + Vector3.Distance(head, Vector3.zero));
			//print ("LeftEye: " + Vector3.Distance(leftEye, Vector3.zero));
			//print ("HeadToCenter: " + Vector3.Distance(centerEye, head));
			//print ("RightEye: " + Vector3.Distance(rightEye, Vector3.zero));
			reformat ();
		}

		int lineidx = 0;
		private void toggleDisplay() {
			if (Input.GetButtonDown ("Tap")) {
//				getConsole ().GetComponent<Renderer> ().enabled = !getConsole ().GetComponent<Renderer> ().enabled;
				println(thescript[lineidx++]);
			}
		}

		private TextMesh getConsole() {
			return gameObject.GetComponent<TextMesh> ();
		}

		private string getText() {
			return getConsole ().text;
		}

		private void setText(string s) {
			getConsole ().text = s;
		}

		void clearConsole () {
			setText ("");
		}

		public void print(string s, bool printToDebugger = false) {
			setText(getText() + s);
			if (printToDebugger) {
				Debug.Log (s);
			}
		}

		public void println() {
			print ("\n");
		}

		public void println(string s, bool printToDebugger = false) {
			print(s + "\n");
			if (printToDebugger) {
				Debug.Log (s);
			}
		}

		void replaceAllInstancesOfChar(char c) {
			setText(getConsole().text.Replace("\n",""));
		}

		int countInstancesOfChar(char c, string s) {
			int count = 0;
			foreach (char cc in s) {
				if (cc.Equals(c)) {
					count++;
				}
			}
			return count;
		}

		void reformat() {
			wrapLines ();
			cull ();
		}

		void wrapLines() {
			if (!linewrapOn)
				return;

			string copy = getText (),
			       build = "";
			string[] strings;

			while (copy.Length > 0) {
				strings = copy.Split (newLineArray, 2);
				build += Regex.Replace (strings[0], ".{"+numCharsPerLine+"}(?!$)", "$0\n");
				build += "\n";
				copy = strings [1];
			}
			setText (build);
		}

		void cull() {
			var temp = getText ();

			string[] strings;

			var numNewLines = countInstancesOfChar ('\n', temp);

			while (numNewLines > numLinesDisplayed) {
				strings = temp.Split(newLineArray, 2);
				privateCache += (strings [0] + '\n');
				temp = strings [1];
				numNewLines--;
			}
			setText (temp);
		}

		string[] thescript = {"Enter GHOST and HAMLET",
			"HAMLET",
			"Where wilt thou lead me? speak; I'll go no further.",
			"GHOST",
			"Mark me.",
			"HAMLET",
			"I will.",
			"GHOST",
			"My hour is almost come,",
			"When I to sulphurous and tormenting flames",
			"Must render up myself.",
			"HAMLET",
			"Alas, poor ghost!",
			"GHOST",
			"Pity me not, but lend thy serious hearing",
			"To what I shall unfold.",
			"HAMLET",
			"Speak; I am bound to hear.",
			"GHOST",
			"So art thou to revenge, when thou shalt hear.",
			"HAMLET",
			"What?",
			"GHOST",
			"I am thy father's spirit,",
			"Doom'd for a certain term to walk the night,",
			"And for the day confined to fast in fires,",
			"Till the foul crimes done in my days of nature",
			"Are burnt and purged away. But that I am forbid",
			"To tell the secrets of my prison-house,",
			"I could a tale unfold whose lightest word",
			"Would harrow up thy soul, freeze thy young blood,",
			"Make thy two eyes, like stars, start from their spheres,",
			"Thy knotted and combined locks to part",
			"And each particular hair to stand on end,",
			"Like quills upon the fretful porpentine:",
			"But this eternal blazon must not be",
			"To ears of flesh and blood. List, list, O, list!",
			"If thou didst ever thy dear father love--",
			"HAMLET",
			"O God!",
			"GHOST",
			"Revenge his foul and most unnatural murder.",
			"HAMLET",
			"Murder!",
			"GHOST",
			"Murder most foul, as in the best it is;",
			"But this most foul, strange and unnatural.",
			"HAMLET",
			"Haste me to know't, that I, with wings as swift",
			"As meditation or the thoughts of love,",
			"May sweep to my revenge.",
			"GHOST",
			"I find thee apt;",
			"And duller shouldst thou be than the fat weed",
			"That roots itself in ease on Lethe wharf,",
			"Wouldst thou not stir in this. Now, Hamlet, hear:",
			"'Tis given out that, sleeping in my orchard,",
			"A serpent stung me; so the whole ear of Denmark",
			"Is by a forged process of my death",
			"Rankly abused: but know, thou noble youth,",
			"The serpent that did sting thy father's life",
			"Now wears his crown.",
			"HAMLET",
			"O my prophetic soul! My uncle!",
			"GHOST",
			"Ay, that incestuous, that adulterate beast,",
			"With witchcraft of his wit, with traitorous gifts,--",
			"O wicked wit and gifts, that have the power",
			"So to seduce!--won to his shameful lust",
			"The will of my most seeming-virtuous queen:",
			"But, soft! methinks I scent the morning air;",
			"Brief let me be. Sleeping within my orchard,",
			"My custom always of the afternoon,",
			"Upon my secure hour thy uncle stole,",
			"With juice of cursed hebenon in a vial,",
			"And in the porches of my ears did pour",
			"The leperous distilment; whose effect",
			"Holds such an enmity with blood of man",
			"That swift as quicksilver it courses through",
			"The natural gates and alleys of the body,",
			"Thus was I, sleeping, by a brother's hand",
			"Of life, of crown, of queen, at once dispatch'd:",
			"Cut off even in the blossoms of my sin,",
			"Unhousel'd, disappointed, unanel'd,",
			"No reckoning made, but sent to my account",
			"With all my imperfections on my head:",
			"HAMLET",
			"O, horrible! O, horrible! most horrible!",
			"GHOST",
			"If thou hast nature in thee, bear it not;",
			"Let not the royal bed of Denmark be",
			"A couch for luxury and damned incest.",
			"But, howsoever thou pursuest this act,",
			"Taint not thy mind, nor let thy soul contrive",
			"Against thy mother aught: leave her to heaven",
			"And to those thorns that in her bosom lodge,",
			"To prick and sting her. Fare thee well at once!",
			"The glow-worm shows the matin to be near,",
			"And 'gins to pale his uneffectual fire:",
			"Adieu, adieu! Hamlet, remember me.",
			"Exit",
			"HAMLET",
			"O all you host of heaven! O earth! what else?",
			"And shall I couple hell? O, fie! Hold, hold, my heart;",
			"And you, my sinews, grow not instant old,",
			"But bear me stiffly up. Remember thee!",
			"Ay, thou poor ghost, while memory holds a seat",
			"In this distracted globe. Remember thee!",
			"Yea, from the table of my memory",
			"I'll wipe away all trivial fond records,",
			"All saws of books, all forms, all pressures past,",
			"That youth and observation copied there;",
			"And thy commandment all alone shall live",
			"Within the book and volume of my brain,",
			"Unmix'd with baser matter: yes, by heaven!",
			"O most pernicious woman!",
			"O villain, villain, smiling, damned villain!",
			"My tables,--meet it is I set it down,",
			"That one may smile, and smile, and be a villain;",
			"At least I'm sure it may be so in Denmark:",
			"Writing",
			"So, uncle, there you are. Now to my word;",
			"It is 'Adieu, adieu! remember me.'",
			"I have sworn 't."};
	}



}